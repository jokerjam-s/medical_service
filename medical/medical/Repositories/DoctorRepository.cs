using medical.DataBase;
using medical.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Repositories
{
    internal class DoctorRepository: IRepository<Doctor>
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext dbContext) { _context = dbContext; }

        public void Add(Doctor entity)
        {
            _context.Doctors.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Doctor entity)
        {
            _context?.Doctors.Remove(entity);
            _context?.SaveChanges();
        }

        public ObservableCollection<Doctor> GetAll()
        {
            _context.Doctors.Load();
            return _context.Doctors.Local.ToObservableCollection(); ;
        }

        public Doctor? GetById(int id)
        {
            return _context.Doctors.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Doctor entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
