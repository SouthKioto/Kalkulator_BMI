using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kalkulator_BMI
{
    public partial class MainPage : ContentPage
    {
        private double result = 0;
        private string txtPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BMIdata.txt");
        // /data/user/0/com.companyname.kalkulator_bmi/files/.config/BMIdata.txt
        private int id = 1;
        private string mess = "Twój stan:";

        public MainPage()
        {
            InitializeComponent();
        }

        private void GoToSaveResultsPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SaveDataPage(txtPath));
        }

        private async void CalculateBMI(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userWeidth_Entry.Text) || string.IsNullOrEmpty(userHeight_Entry.Text))
            {
                await DisplayAlert("Info", "Podaj dane", "OK");
                return;
            }

            double weight = double.Parse(userWeidth_Entry.Text);
            double height = double.Parse(userHeight_Entry.Text);

            double sqrtHeight = Math.Pow(height, 2);
            result = Math.Round((weight / sqrtHeight * 10000), 2);

            if (radio_female.IsChecked)
            {
                if (result < 20)
                {
                    mess += "Niedowaga";
                }
                else if (result >= 20 && result <= 24)
                {
                    mess += "Normalna waga";
                }
                else if (result > 24 && result <= 29)
                {
                    mess += "Nadwaga";
                }
                else if (result > 29 && result <= 39)
                {
                    mess += "Otyłość";
                }
                else if (result > 39)
                {
                    mess += "Nadmierna Otyłość";
                }

                label_score.Text = "Wynik: " + result.ToString();
                label_results.Text = mess;
                saveButton.IsVisible = true;
                saveButton.IsEnabled = true;
            }
            else if (radio_male.IsChecked)
            {
                if (result < 20)
                {
                    mess += "Niedowaga";
                }
                else if (result >= 20 && result <= 24)
                {
                    mess += "Normalna waga";
                }
                else if (result > 24 && result <= 29)
                {
                    mess += "Nadwaga";
                }
                else if (result > 29 && result <= 39)
                {
                    mess += "Otyłość";
                }
                else if (result > 39)
                {
                    mess += "Nadmierna Otyłość";
                }

                label_score.Text = "Wynik: " + result.ToString();
                label_results.Text = mess;
                saveButton.IsVisible = true;
                saveButton.IsEnabled = true;

                Console.WriteLine("XD" + txtPath);
            }
            else
            {
                await DisplayAlert("Info", "Wybierz płeć", "OK");
            }
        }

        private void SaveData(object sender, EventArgs e)
        {
            string data = id + ";" + result + ";" + mess + ";";

            if (!File.Exists(txtPath))
            {
                List<string> list = new List<string> { data };
                File.WriteAllLines(txtPath, list);
            }
            else
            {
                List<string> list = File.ReadAllLines(txtPath).ToList();

                string lastLine = list.Last();
                string[] splitedData = lastLine.Split(';');
                int lastId = int.Parse(splitedData[0]);
                lastId += 1;

                string newData = lastId.ToString() + ";" + result + ";" + mess + ";";
                list.Add(newData);

                File.WriteAllLines(txtPath, list);
            }
        }

    }
}
