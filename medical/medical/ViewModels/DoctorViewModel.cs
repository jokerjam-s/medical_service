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

namespace medical.ViewModels
{
    internal class DoctorViewModel: INotifyPropertyChanged
    {

        private DoctorRepository _repository;
        private Doctor? selectedDoctor;

        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;

        public ObservableCollection<Doctor> Doctors { get; set; }

        public Doctor? SelectedDoctor
        {
            get { return selectedDoctor; }
            set { selectedDoctor = value; OnPropertyChanged(nameof(SelectedDoctor)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public DoctorViewModel()
        {
            _repository = new DoctorRepository(new AppDbContext());
            Doctors = _repository.GetAll();
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      DoctorSingleWindow pWindow = new DoctorSingleWindow(new Doctor());
                      if (pWindow.ShowDialog() == true)
                      {
                          Doctor doc = pWindow.doctor;
                          _repository.Add(doc);
                      }
                  }));
            }
        }


        // команда редактирования
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Doctor? doc = selectedItem as Doctor;
                      if (doc == null) return;

                      Doctor vm = new Doctor
                      {
                          Id = doc.Id,
                          FullName = doc.FullName,
                          Specialization = doc.Specialization,
                          LicenseNumber = doc.LicenseNumber,
                          Schedule = doc.Schedule,
                      };
                      DoctorSingleWindow pWindow = new DoctorSingleWindow(doc);


                      if (pWindow.ShowDialog() == true)
                      {
                          doc.FullName = pWindow.doctor.FullName;
                          doc.Specialization = pWindow.doctor.Specialization;
                          doc.Schedule = pWindow.doctor.Schedule;
                          doc.LicenseNumber = pWindow.doctor.LicenseNumber;

                          _repository.Update(doc);
                      }
                  }));
            }
        }


        // команда удаления
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand((selectedItem) =>
                  {
                      // получаем выделенный объект
                      Doctor? doc = selectedItem as Doctor;
                      if (doc == null) return;
                      _repository.Delete(doc);
                  }));
            }
        }

    }
}
