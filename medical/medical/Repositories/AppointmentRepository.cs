using medical.DataBase;
using medical.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical.Repositories
{
    internal class AppointmentRepository: IRepository<Appointment>
    {
        private AppDbContext _context;
        public AppointmentRepository(AppDbContext context) { _context = context; }

        public void Add(Appointment entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Appointment entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public ObservableCollection<Appointment> GetAll()
        {
            _context.Appointments.Load();
            return _context.Appointments.Local.ToObservableCollection();
        }

        public void Update(Appointment entity)
        {
            
            _context.Appointments.Update(entity);
            _context.SaveChanges();
        }


        public ObservableCollection<DoctorStat> GetStaistic()
        {
            List<DoctorStat> stats = new List<DoctorStat>();

            var sql = _context.Database.GetDbConnection().CreateCommand();
            sql.CommandText = "SELECT a.DoctorId, d.FullName, Count(*) AS CountPriem FROM Appointments a, Doctors d WHERE a.DoctorId = d.Id GROUP BY a.DoctorId, d.FullName";
            sql.CommandType = System.Data.CommandType.Text;
            sql.Connection.Open();
            var sqlData = sql.ExecuteReader();

            while (sqlData.Read())
            {
                DoctorStat stat = new DoctorStat
                {
                    DoctorId = sqlData.GetInt32(0),
                    FullName = sqlData.GetString(1),
                    CountPriem = sqlData.GetInt32(2)
                };

                stats.Add(stat);
            }

            sql.Connection.Close();
            return new ObservableCollection<DoctorStat>(stats);
        }

        public ObservableCollection<ServiceStat> GetStaistic_2()
        {
            List<ServiceStat> stats = new List<ServiceStat>();

            var sql = _context.Database.GetDbConnection().CreateCommand();
            sql.CommandText = "SELECT a.ServiceId, s.Name, SUM(s.Cost) AS Costs FROM Appointments a, MedicalServices s WHERE a.ServiceId = s.Id GROUP BY a.ServiceId, s.Name";
            sql.CommandType = System.Data.CommandType.Text;
            sql.Connection.Open();
            var sqlData = sql.ExecuteReader();

            while (sqlData.Read())
            {
                ServiceStat stat = new ServiceStat
                {
                    ServiceId = sqlData.GetInt32(0),
                    Name = sqlData.GetString(1),
                    Costs = sqlData.GetDecimal(2)
                };

                stats.Add(stat);
            }

            sql.Connection.Close();
            return new ObservableCollection<ServiceStat>(stats);
        }


        public ObservableCollection<Appointment> GetByDay(DateTime date)
        {
            //_context.Appointments.Load();
            List<Appointment> lst = _context.Appointments.Where(x => x.Priem.Date == date.Date).AsNoTracking().ToList();

            return new ObservableCollection<Appointment>(lst);

        }

    }
}
