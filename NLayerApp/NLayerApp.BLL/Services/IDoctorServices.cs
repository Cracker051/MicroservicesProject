using NLayerApp.BLL.DTO;

namespace NLayerApp.BLL.Services
{
    public interface IDoctorServices
    {
        ICollection<DoctorsDTO> GetDoctors();
        DoctorsDTO GetDoctorById(int id);
        ICollection<DoctorsDTO> GetDoctorsBySurname(string surname);
        ICollection<DoctorsDTO> GetDoctorsByDepartment(string departmentName);
        DoctorsDTO CreateDoctor(DoctorsDTO doctor);
        DoctorsDTO UpdateDoctor(int doctorId, DoctorsDTO doctors);
        bool DeleteDoctor(int doctorId);
    }
}
