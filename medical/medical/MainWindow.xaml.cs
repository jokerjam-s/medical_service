using medical.ViewModels;
using medical.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace medical
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AppointmentViewModel viewModel = new AppointmentViewModel();

        public MainWindow()
        {
            InitializeComponent();
            grAppointments.DataContext = viewModel;
            clPacient.ItemsSource = viewModel.Patients;
            clDoctor.ItemsSource = viewModel.Doctors;
            clService.ItemsSource = viewModel.Services;

            DataContext = viewModel;
        }

        private void btnPatient_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow patients = new PatientsWindow();
            patients.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorsWindow docform = new DoctorsWindow();
            docform.Show();
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            ServicesWindow servicesWindow = new ServicesWindow();
            servicesWindow.Show();
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? date = clSelect.SelectedDate;

            if (date != null)
            {
                stBarMsg.Text = "Чтение данных";
                viewModel.SelectCommand.Execute(date);
                stBarMsg.Text = string.Empty;
            }
        }

        private void StatItem_Click(object sender, RoutedEventArgs e)
        {
            StatisticWindow window = new StatisticWindow();
            window.Show();
        }

        private void Stat_2_Item_Click(object sender, RoutedEventArgs e)
        {
            Stst_2_Window window = new Stst_2_Window();
            window.Show();
        }
    }
}