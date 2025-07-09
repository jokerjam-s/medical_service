using medical.DataBase;
using medical.Models;
using medical.Repositories;
using medical.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace medical.ViewModels
{
    internal class AppointmentViewModel : INotifyPropertyChanged
    {
        private readonly AppointmentRepository _appointment;
        private readonly DoctorRepository _doctor;
        private readonly PatientRepository _patient;
        private readonly MedicalServiceRepository _medical;

        private DateTime? lastDateTime { get; set; } = null;

        private Appointment? selectedAppointment;
        public ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();
        public List<MedicalService> Services { get; set; }
        public List<Doctor> Doctors { get; set; }
        public List<Patient> Patients { get; set; }

        public int Hour = 0;
        public int Minute = 0;

        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;
        RelayCommand? selectCommand;

        public Appointment? SelectedAppointment
        {
            get => selectedAppointment;
            set { selectedAppointment = value; OnPropertyChanged(nameof(SelectedAppointment)); }
        }

        public AppointmentViewModel()
        {
            AppDbContext context = new AppDbContext();

            _appointment = new AppointmentRepository(context);
            _doctor = new DoctorRepository(context);
            _patient = new PatientRepository(context);
            _medical = new MedicalServiceRepository(context);

            //Appointments = _appointment.GetAll();
            Services = _medical.GetAll().ToList();
            Patients = _patient.GetAll().ToList();
            Doctors = _doctor.GetAll().ToList();
        }
        
        private void GetByDay(DateTime day)
        {
            ObservableCollection<Appointment> appointments = _appointment.GetByDay(day);

            Appointments.Clear();

            foreach(Appointment appointment in appointments)
            {

                Appointment app = new Appointment
                {
                    Id = appointment.Id,
                    DoctorId = appointment.DoctorId,
                    PatientId = appointment.PatientId,
                    Priem = appointment.Priem,
                    Time = appointment.Time,
                    ServiceId = appointment.ServiceId,
                    Status = appointment.Status,
                };
                                
                Appointments.Add(app);
            }
        }
        
        /*
        public AttributeTargets Statistic()
        {
            
        }
        */
        
        public RelayCommand SelectCommand
        {
            get
            {
                return selectCommand ??
                  (selectCommand = new RelayCommand((dateTime) =>
                  {
                      DateTime date = (DateTime)dateTime;
                      lastDateTime = date;
                      GetByDay(date);
                  }));
                
            }
        
        }
        
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new RelayCommand((o) =>
                {
                    AppointmentWindow pWindow = new AppointmentWindow(new Appointment());
                    if (pWindow.ShowDialog() == true)
                    {
                        Appointment app = pWindow.appointment;

                        _appointment.Add(app);
                        if (lastDateTime != null)
                        {
                            GetByDay((DateTime)lastDateTime);
                        }
                    }
                }));
            }
        }


        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Appointment? app = selectedItem as Appointment;
                      if (app == null) return;

                      Appointment? vm = new Appointment
                      {
                          Id = app.Id,
                          Priem = app.Priem,
                          Time = app.Time,
                          DoctorId = app.DoctorId,
                          PatientId = app.PatientId,
                          ServiceId = app.ServiceId,
                          Status = app.Status
                      };

                      AppointmentWindow pWindow = new AppointmentWindow(vm);

                      if (pWindow.ShowDialog() == true)
                      {
                          vm.Priem = pWindow.appointment.Priem;
                          vm.Time = pWindow.appointment.Time;
                          vm.DoctorId = pWindow.appointment.DoctorId;
                          vm.PatientId = pWindow.appointment.PatientId;
                          vm.ServiceId = pWindow.appointment.ServiceId;
                          vm.Status = pWindow.appointment.Status;

                          _appointment.Update(vm);
                          if (lastDateTime != null)
                          {
                              GetByDay((DateTime)lastDateTime);
                          }
                          vm = null;
                      }
                  }));
            }
        }


        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((selectedItem) =>
                    {
                        Appointment? appointment = selectedItem as Appointment;

                        if(appointment ==  null) return;

                        _appointment.Delete(appointment);
                    }
                    ));
            }
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
