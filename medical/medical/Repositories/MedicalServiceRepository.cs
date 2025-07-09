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
    internal class MedicalServiceRepository : IRepository<MedicalService>
    {
        private readonly AppDbContext _context;

        public MedicalServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(MedicalService entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(MedicalService entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public ObservableCollection<MedicalService> GetAll()
        {
            _context.MedicalServices.Load();
            return _context.MedicalServices.Local.ToObservableCollection(); ;
        }

        public MedicalService? GetById(int id)
        {
            return _context.MedicalServices.FirstOrDefault(x => x.Id == id);
        }

        public void Update(MedicalService entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
