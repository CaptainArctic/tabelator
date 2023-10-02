using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;

namespace LoadDataGrid
{
    public partial class MainWindow : Window
    {
        public class People
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public JobType Job_type { get; set; }
            public List<WorkTime> Work_times { get; set; }
            public int DaysWorked { get; set; }
        }

        public enum JobType
        {
            Программист,
            Работник_call_центра,
            Консультант
        }

        public class WorkTime
        {
            public DateTime Start_time { get; set; }
            public DateTime End_time { get; set; }
        }

        Dictionary<string, string> monthFiles = new Dictionary<string, string>()
    {
        { "Январь", "January.JSON" },
        { "Февраль", "February.JSON" },
        { "Март", "March.JSON" },
        { "Апрель", "April.JSON" },
        { "Май", "May.JSON" },
        { "Июнь", "June.JSON" },
        { "Июль", "Jule.JSON" },
        { "Август", "August.JSON" },
        { "Сентябрь", "September.JSON" },
        { "Октябрь", "October.JSON" },
        { "Ноябрь", "November.JSON" },
        { "Декабрь", "December.JSON" }
    };

        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            string fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "members.JSON");

            if (File.Exists(fname) == false)
            {
                List<People> peopleList = new List<People>();
                string lis = JsonConvert.SerializeObject(peopleList);
                File.WriteAllText(fname, lis);
            }

            var json = File.ReadAllText(fname);
            List<People>? _people = JsonConvert.DeserializeObject<List<People>>(json);


            //clear out grid
            GridPeople.ItemsSource = null;

            if (_people != null)
            {
                GridPeople.ItemsSource = _people;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var data = (List<People>)this.GridPeople.ItemsSource;

            var json = JsonConvert.SerializeObject(data, Formatting.Indented);

            string selectedPage = ((ComboBoxItem)CbPages.SelectedItem).Content.ToString();
            string fname = "";

            if (monthFiles.TryGetValue(selectedPage, out string fileName))
            {
                fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            }

            if (File.Exists(fname) == true)
            {
                File.Delete(fname);
            }

            File.WriteAllText(fname, json);
        }

        private void CbPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedPage = ((ComboBoxItem)CbPages.SelectedItem).Content.ToString();

            string fname = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, monthFiles[selectedPage]);

            if (File.Exists(fname) == false)
            {
                List<People> peopleList = new List<People>();
                string lis = JsonConvert.SerializeObject(peopleList);
                File.WriteAllText(fname, lis);
            }

            var json = File.ReadAllText(fname);
            List<People>? _people = JsonConvert.DeserializeObject<List<People>>(json);

            //clear out grid
            GridPeople.ItemsSource = null;

            if (_people != null)
            {
                GridPeople.ItemsSource = _people;
            }
        }

    }
}