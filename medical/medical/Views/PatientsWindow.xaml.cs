using medical.DataBase;
using medical.Models;
using medical.ViewModels;
using Microsoft.EntityFrameworkCore;
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

namespace medical
{
    /// <summary>
    /// Логика взаимодействия для PatientsWindow.xaml
    /// </summary>
    public partial class PatientsWindow : Window
    {
        private PatientViewModel viewModel = new PatientViewModel();

        public PatientsWindow()
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
