using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kalkulator_BMI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveDataPage : ContentPage
    {
        private List<BMIData> data = new List<BMIData>();
        private string txtPath;

        public SaveDataPage(string txtPath)
        {
            InitializeComponent();
            this.txtPath = txtPath;
            bmidata_List.ItemsSource = data;
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(txtPath))
            {
                List<string> readData = File.ReadAllLines(txtPath).ToList();

                foreach (string line in readData)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] splited = line.Split(';');

                        int id = int.Parse(splited[0]);
                        float result = float.Parse(splited[1]);
                        string mess = splited[2];

                        if (splited.Length > 1)
                        {
                            BMIData bmidata = new BMIData
                            {
                                Id = id,
                                Result = result,
                                Message = mess
                            };
                            data.Add(bmidata);
                        }
                    }
                }
            }
        }

        private async void DeleteData(object sender, EventArgs e)
        {
            var result = await DisplayAlert("Query", "U want delete this data", "Yes", "No");

            if(result)
            {
                var selectedItem = (BMIData)bmidata_List.SelectedItem;

                if (selectedItem != null)
                {
                    data.Remove(selectedItem);
                    List<string> lines = new List<string>();
                    foreach (var item in data)
                    {
                        lines.Add($"{item.Id};{item.Result};{item.Message}");
                    }
                    File.WriteAllLines(txtPath, lines);
                }
            }
        }
    }
}
