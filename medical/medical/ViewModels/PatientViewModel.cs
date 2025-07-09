using medical.DataBase;
using medical.Models;
using Microsoft.EntityFrameworkCore;
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
    internal class PatientViewModel : INotifyPropertyChanged
    {
        private PatientRepository _repository;
        private Patient? selectedPatient;

        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;

        public ObservableCollection<Patient> Patients { get; set; }

        public Patient? SelectedPatient
        {
            get { return selectedPatient; }
            set { selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); }
        }

        public PatientViewModel()
        {
            _repository = new PatientRepository(new AppDbContext());
            Patients = _repository.GetAll();
        }

        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      PatientWindow pWindow = new PatientWindow(new Patient());
                      if (pWindow.ShowDialog() == true)
                      {
                          Patient patient = pWindow.Patient;
                          _repository.Add(patient);
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
                      Patient? patient = selectedItem as Patient;
                      if (patient == null) return;

                      Patient vm = new Patient
                      {
                          Id = patient.Id,
                          FullName = patient.FullName,
                          BirthDate = patient.BirthDate
                      };
                      PatientWindow pWindow = new PatientWindow(vm);


                      if (pWindow.ShowDialog() == true)
                      {
                          patient.FullName = pWindow.Patient.FullName;
                          patient.BirthDate = pWindow.Patient.BirthDate;
                          _repository.Update(patient);
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
                      Patient? patient = selectedItem as Patient;
                      if (patient == null) return;
                     _repository.Delete(patient);
                  }));
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
