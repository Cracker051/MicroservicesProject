using AutoMapper;
using NLayerApp.BLL.DTO;
using NLayerApp.BLL.Exceptions;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;

namespace NLayerApp.BLL.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentServices(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public ICollection<DepartmentsDTO> GetDepartments()
        {
            var entities = _departmentRepository.GetDepartments();
            return _mapper.Map<ICollection<DepartmentsDTO>>(entities);
        }
        public DepartmentsDTO GetById(int id)
        {
            
            if (!_departmentRepository.DepartmentExist(id))
            {
                throw new NotFoundException(nameof(Departments), id);
            }
            var entity = _departmentRepository.GetDepartmentById(id);
            return _mapper.Map<DepartmentsDTO>(entity);
        }
        public DepartmentsDTO GetByName(string name)
        {
            if (!_departmentRepository.DepartmentExist(name))
            {
                throw new NotFoundException(nameof(Departments), name);
            }
            var entity = _departmentRepository.GetDepartmentByName(name);
            return _mapper.Map<DepartmentsDTO>(entity);
        }
        public DepartmentsDTO Create(DepartmentsDTO department)
        {
            if (_departmentRepository.DepartmentExist(department.Name))
            {
                throw new ArleadyExistsException(nameof(Departments), department.Name);
            }
            var entity = _mapper.Map<Departments>(department);
            if(!_departmentRepository.CreateDepartment(entity))
            {
                throw new ModelErrorException("Smth went wrong!");
            }
            return _mapper.Map<DepartmentsDTO>(entity);  
        }

        public DepartmentsDTO Update(int departmentId,DepartmentsDTO department)
        {
            if (departmentId != department.Id)
            {
                throw new ArgumentException("Id's arent equal");
            }
            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                throw new NotFoundException(nameof(Departments), departmentId);
            }
            var departmentMap = _mapper.Map<Departments>(department);
            if (_departmentRepository.UpdateDepartment(departmentMap))
            {
                throw new ModelErrorException("Smth went wrong!");
            }
            return _mapper.Map<DepartmentsDTO>(departmentMap);
        }

        public bool Delete(int departmentId)
        {
            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                throw new NotFoundException(nameof(Departments), departmentId);
            }
            var departmentToDelete = _departmentRepository.GetDepartmentById(departmentId);
            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                throw new ModelErrorException("Smth went wrong!");
            }
            return true;

        }
    }
}
