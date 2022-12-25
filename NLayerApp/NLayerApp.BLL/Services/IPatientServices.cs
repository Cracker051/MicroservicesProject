using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Exceptions;
using NLayerApp.DAL.Entities;

namespace NLayerApp.BLL.Services
{
	public interface IPatientServices
	{
        public ICollection<PatientsDTO> GetPatients();
        public PatientsDTO GetPatientById(int id);
        public ICollection<PatientsDTO> GetPatientsBySurname(string surname);
        public ICollection<PatientsDTO> GetPatientsByDiagnosis(string diagnosis);
        public PatientsDTO CreatePatient(PatientsDTO patient);
        public PatientsDTO UpdatePatient(int patientId, PatientsDTO patient);
        public bool DeletePatient(int patientId);
    }
}

