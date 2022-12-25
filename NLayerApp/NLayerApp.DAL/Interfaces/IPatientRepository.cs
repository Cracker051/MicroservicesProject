using NLayerApp.DAL.Entities;

namespace NLayerApp.DAL.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patients> GetPatients();
        Patients GetPatientById(int id);
        ICollection<Patients> GetPatientsBySurname(string surname);
        ICollection<Patients> GetPatientsByDiagnosis(string diagnosis);
        bool PatientExist(int id);
        bool Save();
        bool Create(Patients patient);
        bool Update(Patients patient);
        bool Delete(Patients patient);
    }
}
