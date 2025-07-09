using medical.DataBase;
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
    public class Appointment: INotifyPropertyChanged
    {
        private int id;
        private int patientId;
        private int doctorId;
        private int serviceId;
        private DateTime priem;
        private string time;
        private string? status;



        public int Id 
        {
            get { return id; } 
            set { id = value; OnPropertyChanged("Id"); }
        }

        public int PatientId
        {
            get { return patientId; }
            set { patientId = value; OnPropertyChanged("PatientId"); }
        }

        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value; OnPropertyChanged("DoctorId"); }
        }

        public DateTime Priem
        {
            get { return priem; }
            set { priem = value; OnPropertyChanged(nameof(Priem)); }
        }

        public string Time
        {
            get { return time; }
            set { time = value; OnPropertyChanged(nameof(Time)); }
        }

        public int ServiceId
        {
            get { return serviceId; }
            set { serviceId = value; OnPropertyChanged(nameof(ServiceId)); }
        }

        public string? Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(nameof(Status)); }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
