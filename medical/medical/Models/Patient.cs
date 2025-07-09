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
    public class Patient: INotifyPropertyChanged
    {
        private int id;
        private string fullName = String.Empty;
        private DateTime birthDate;
        private string? phone;
        private string? email;
        private string? medicalHistory;

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
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string? Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public string? Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string? MedicalHistory
        {
            get { return medicalHistory; }
            set 
            { 
                medicalHistory = value;
                OnPropertyChanged("MedicalHistory");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
