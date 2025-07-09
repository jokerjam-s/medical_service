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
    internal class MedicalServiceViewModel : INotifyPropertyChanged
    {
        private MedicalServiceRepository _repository;
        private MedicalService? selectedService;
        public ObservableCollection<MedicalService> MedicalServices { get; set; }

        RelayCommand? addCommand;
        RelayCommand? editCommand;
        RelayCommand? deleteCommand;

        public MedicalServiceViewModel()
        {
            _repository = new MedicalServiceRepository(new DataBase.AppDbContext());
            MedicalServices = _repository.GetAll();
        }


        // команда добавления
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      ServiceSingleWindow pWindow = new ServiceSingleWindow(new MedicalService());
                      if (pWindow.ShowDialog() == true)
                      {
                          MedicalService service = pWindow.service;
                          _repository.Add(service);
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
                      MedicalService? service = selectedItem as MedicalService;
                      if (service == null) return;

                      MedicalService vm = new MedicalService
                      {
                          Id = service.Id,
                          Name = service.Name,
                          Cost = service.Cost,
                          Duration = service.Duration
                      };
                      ServiceSingleWindow pWindow = new ServiceSingleWindow(vm);


                      if (pWindow.ShowDialog() == true)
                      {
                          service.Name = pWindow.service.Name;
                          service.Cost = pWindow.service.Cost;
                          service.Duration = pWindow.service.Duration;
                          _repository.Update(service);
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
                      MedicalService? service = selectedItem as MedicalService;
                      if (service == null) return;
                      _repository.Delete(service);
                  }));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public MedicalService? SelectedService
        {
            get { return selectedService; }
            set { selectedService = value; OnPropertyChanged(nameof(SelectedService)); }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
