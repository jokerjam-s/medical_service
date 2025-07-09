using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace medical.Models
{
    public class Doctor : INotifyPropertyChanged
    {
        private int id;
        private string fullName;
        private string specialization;
        private string? schedule;
        private string? licenseNumber;

        public int Id
        {
            get { return id; }
            set 
            {
                id = value; 
                OnPropertyChanged("Id");
            }
        }

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value; OnPropertyChanged("FullName"); 
            }
        }

        public string Specialization
        {
            get { return specialization; }
            set 
            { 
                specialization = value; OnPropertyChanged(nameof(Specialization));
            }
        }

        public string? Schedule
        {
            get { return schedule; }
            set
            {
                schedule = value; OnPropertyChanged(nameof(Schedule));
            }
        }

        public string? LicenseNumber
        {
            get { return licenseNumber; }
            set
            {
                licenseNumber = value; OnPropertyChanged(nameof(LicenseNumber));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
