using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Exceptions;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using NLayerApp.DAL.Repository;

namespace NLayerApp.BLL.Services
{
	public class PatientServices:IPatientServices
	{
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientServices(IDoctorRepository doctorRepository, IPatientRepository patientRepository, IMapper mapper)
		{
			_doctorRepository = doctorRepository;
			_patientRepository = patientRepository;
			_mapper = mapper;
		}
		public ICollection<PatientsDTO> GetPatients()
		{
			var entites = _patientRepository.GetPatients();
			return _mapper.Map<ICollection<PatientsDTO>>(entites);
		}
		public PatientsDTO GetPatientById(int id)
		{
			if (!_patientRepository.PatientExist(id))
			{
				throw new NotFoundException(nameof(Patients), id);
			}
			var entity = _patientRepository.GetPatientById(id);
			return _mapper.Map<PatientsDTO>(entity);
		}
		public ICollection<PatientsDTO> GetPatientsBySurname(string surname)
		{
			var entities = _patientRepository.GetPatientsBySurname(surname);
			if (!entities.Any())
			{
                throw new NotFoundException(nameof(Patients), surname);
            }
			return _mapper.Map<ICollection<PatientsDTO>>(entities);
		}
        public ICollection<PatientsDTO> GetPatientsByDiagnosis(string diagnosis)
        {
			var entities = _patientRepository.GetPatientsByDiagnosis(diagnosis);
            if (!entities.Any())
            {
                throw new NotFoundException(nameof(Patients), diagnosis);
            }
            return _mapper.Map<ICollection<PatientsDTO>>(entities);
        }
        public PatientsDTO CreatePatient(PatientsDTO patient)
        {
            if (!_doctorRepository.DoctorExist(patient.DoctorId))
            {
                throw new NotFoundException(nameof(Doctors), patient.DoctorId);
            }
            var entity = _mapper.Map<Patients>(patient);
            _patientRepository.Create(entity);
            return _mapper.Map<PatientsDTO>(entity);
        }
        public PatientsDTO UpdatePatient(int patientId, PatientsDTO patient)
        {
            if (!_patientRepository.PatientExist(patientId))
            {
                throw new NotFoundException(nameof(Patients), patient);
            }
            if (patientId != patient.Id)
            {
                throw new ArgumentException("Ids arent equal!");
            }
            if (!_doctorRepository.DoctorExist(patient.DoctorId))
            {
                throw new NotFoundException(nameof(Doctors), patient.DoctorId);
            }
            var entity = _mapper.Map<Patients>(patient);
            if (!_patientRepository.Update(entity)) 
            {
                throw new ModelErrorException("Smth went wrong");
            }
            return _mapper.Map<PatientsDTO>(entity);
        }
        public bool DeletePatient(int patientId) { 
            if (!_patientRepository.PatientExist(patientId))
            {
                throw new NotFoundException(nameof(Patients), patientId);
            }
            var patientToDelete = _patientRepository.GetPatientById(patientId);
            if (!_patientRepository.Delete(patientToDelete))
            {
                throw new ModelErrorException("Smth went wrong!");
            }
            return true;

        }

    }
}

