using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Exceptions;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace NLayerApp.BLL.Services
{
    public class DoctorServices:IDoctorServices
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DoctorServices(IDoctorRepository doctorRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public ICollection<DoctorsDTO> GetDoctors()
        {
            var entities = _doctorRepository.GetDoctors();
            return _mapper.Map<ICollection<DoctorsDTO>>(entities);
        }
        public DoctorsDTO GetDoctorById(int id)
        {
            if (!_doctorRepository.DoctorExist(id))
            {
                throw new NotFoundException(nameof(Doctors), id);
            }
            var entity = _doctorRepository.GetDoctorById(id);
            return _mapper.Map<DoctorsDTO>(entity);
        }
        public ICollection<DoctorsDTO> GetDoctorsBySurname(string surname)
        {
            var entities= _doctorRepository.GetDoctorsBySurname(surname);
            if (!entities.Any())
            {
                throw new NotFoundException(nameof(Doctors), surname);
            }
            return _mapper.Map<ICollection<DoctorsDTO>>(entities);
        }
        public ICollection<DoctorsDTO> GetDoctorsByDepartment(string departmentName)
        {
            var entities = _doctorRepository.GetDoctorsByDepartment(departmentName);
            if (!entities.Any())
            {
                throw new NotFoundException(nameof(Doctors), departmentName);
            }
            return _mapper.Map<ICollection<DoctorsDTO>>(entities);
        }
        public DoctorsDTO CreateDoctor(DoctorsDTO doctors)
        {
            if (!_departmentRepository.DepartmentExist(doctors.DepartmentId))
            {
                throw new NotFoundException(nameof(Departments),doctors.DepartmentId);
            }
            var entity = _mapper.Map<Doctors>(doctors);
            _doctorRepository.CreateDoctor(entity);
            return _mapper.Map<DoctorsDTO>(entity);
        }
        public DoctorsDTO UpdateDoctor(int doctorId,DoctorsDTO doctors)
        {
            if (!_doctorRepository.DoctorExist(doctorId))
            {
                throw new NotFoundException(nameof(Doctors), doctorId);
            }
            if (doctorId != doctors.Id)
            {
                throw new ArgumentException("Ids arent equal!");
            }
            if (!_departmentRepository.DepartmentExist(doctors.DepartmentId))
            {
                throw new NotFoundException(nameof(Departments), doctors.DepartmentId);
            }
            var doctor = _mapper.Map<Doctors>(doctors);
            if (!_doctorRepository.UpdateDoctor(doctor))
            {
                throw new ModelErrorException("Smth went wrong");
            }
            return _mapper.Map<DoctorsDTO>(doctor);
        }
        public bool DeleteDoctor(int doctorId)
        {
            if (!_doctorRepository.DoctorExist(doctorId))
            {
                throw new NotFoundException(nameof(Doctors), doctorId);
            }
            var doctorToDelete = _doctorRepository.GetDoctorById(doctorId);
            if (!_doctorRepository.DeleteDoctor(doctorToDelete))
            {
                throw new ModelErrorException("Smth went wrong!");
            }
            return true;

        }
    }
}

