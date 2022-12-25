using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctors> GetDoctors();
        Doctors GetDoctorById(int id);
        ICollection<Doctors> GetDoctorsBySurname(string name);
        Doctors GetDoctorByEmail(string email);
        ICollection<Doctors> GetDoctorsByDepartment(string DepartmentName);
        bool DoctorExist(int doctorId);
        bool DoctorExist(string doctorName);
        bool CreateDoctor(Doctors entity);
        bool UpdateDoctor(Doctors doctor);
        bool DeleteDoctor(Doctors doctor);
        bool Save();
    }
}
