using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace NLayerApp.DAL.Repository
{
    public class DoctorRepository:IDoctorRepository
    {
        private readonly DataContext _context;
        public DoctorRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Doctors> GetDoctors() 
        {
            return _context.Doctors.OrderBy(p => p.Id).Include(p => p.Department).ToList();
        }
        public bool DoctorExist(int DoctorId)
        {
            return _context.Doctors.Any(p => p.Id == DoctorId);
        }
        public bool DoctorExist(string DoctorName)
        {
            return _context.Doctors.Any(p => p.Name == DoctorName);
        }
        public Doctors GetDoctorById(int id)
        {
            return _context.Doctors.Include(p=>p.Department).Where(p => p.Id == id).FirstOrDefault();
        }
        public ICollection<Doctors> GetDoctorsBySurname(string surname)
        {
            return _context.Doctors.Include(p => p.Department).Where(p => p.Surname == surname).ToList();
        }
        public Doctors GetDoctorByEmail(string email)
        {
            return _context.Doctors.Include(p => p.Department).Where(p => p.Email == email).FirstOrDefault();
        }
        public ICollection<Doctors> GetDoctorsByDepartment(string DepartmentName)
        {
            return _context.Doctors.Include(p => p.Department).Where(p => p.Department.Name == DepartmentName).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool CreateDoctor(Doctors doctor)
        {
            _context.Add(doctor);
            return Save();
        }
        public bool UpdateDoctor(Doctors doctor)
        {
            _context.Update(doctor);
            return Save();
        }
        public bool DeleteDoctor(Doctors doctor)
        {
            _context.Remove(doctor);
            return Save();
        }
    }
}
