using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

namespace NLayerApp.DAL.Repository
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly DataContext _context;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }
        public Departments GetDepartmentById(int id)
        {
            return _context.Departments.Include(d => d.Doctors).Where(p => p.Id == id).FirstOrDefault();
        }
        public Departments GetDepartmentByName(string name)
        {
            return _context.Departments.Include(d=>d.Doctors).Where(p => p.Name == name).FirstOrDefault();
        }
        public bool DepartmentExist(int departmentId)
        {
            return _context.Departments.Any(p => p.Id == departmentId);
        }
        public bool DepartmentExist(string departmentName)
        {
            return _context.Departments.Any(p => p.Name == departmentName);
        }
        public ICollection<Departments> GetDepartments()
        {
            return _context.Departments.Include(d => d.Doctors).OrderBy(p => p.Id).ToList();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool CreateDepartment(Departments department)
        {
            _context.Add(department);
            return Save();
        }
        public bool UpdateDepartment(Departments department)
        {
            _context.Update(department);
            return Save();
        }
        public bool DeleteDepartment(Departments department)
        {
            _context.Remove(department);
            return Save();
        }

    }
}
