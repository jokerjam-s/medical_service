using medical.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для DoctorSingleWindow.xaml
    /// </summary>
    public partial class DoctorSingleWindow : Window
    {
        public Doctor doctor { get; set; }

        public DoctorSingleWindow(Doctor doc)
        {
            InitializeComponent();
            this.doctor = doc;
            DataContext = doctor;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
