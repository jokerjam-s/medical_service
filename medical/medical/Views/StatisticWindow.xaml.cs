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
    /// Логика взаимодействия для StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {

        private AppointmentRepository _repository = new AppointmentRepository(new DataBase.AppDbContext());

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }

        public StatisticWindow()
        {
            InitializeComponent();

            ObservableCollection<DoctorStat> stats = _repository.GetStaistic();

            // Заполняем метки и значения
            Labels = new List<string>();
            var values = new ChartValues<double>();


            foreach (var item in stats)
            {
                Labels.Add(item.FullName);
                values.Add(item.CountPriem);
            }

            // Создаем коллекцию серий (столбцов)
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Доктора",
                    Values = values
                }
            };

            // Устанавливаем DataContext для привязки данных
            DataContext = this;

        }
    }
}
