using LiveCharts;
using LiveCharts.Wpf;
using medical.DataBase;
using medical.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace medical.Windows
{
    /// <summary>
    /// Логика взаимодействия для Stst_2_Window.xaml
    /// </summary>
    /// 
    public partial class Stst_2_Window : Window
    {
        private AppointmentRepository _repository = new AppointmentRepository(new DataBase.AppDbContext());
        public SeriesCollection SeriesCollection { get; set; }

        public Stst_2_Window()
        {
            InitializeComponent();

            ObservableCollection<ServiceStat> stats = _repository.GetStaistic_2();

            // Создаем коллекцию серий для PieChart
            SeriesCollection = new SeriesCollection();

            foreach (var item in stats)
            {
                // Добавляем сегмент в круговую диаграмму
                SeriesCollection.Add(new PieSeries
                {
                    Title = item.Name,
                    Values = new ChartValues<decimal> { item.Costs },
                    DataLabels = true // показывать метки на сегментах
                });
            }

            DataContext = this;
        }
    }
}
