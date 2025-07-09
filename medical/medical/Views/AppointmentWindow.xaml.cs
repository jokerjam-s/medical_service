using medical.Models;
using medical.ViewModels;
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
    /// Логика взаимодействия для AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        public Appointment appointment {  get; set; }

        private MedicalServiceViewModel services = new MedicalServiceViewModel();
        private DoctorViewModel doctors = new DoctorViewModel();
        private PatientViewModel patients = new PatientViewModel();

        public AppointmentWindow(Appointment item)
        {
            InitializeComponent();
            appointment = item;
            DataContext = appointment;    
          
            cbDoctor.ItemsSource = doctors.Doctors;
            cbPatient.ItemsSource = patients.Patients;
            cbService.ItemsSource = services.MedicalServices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
