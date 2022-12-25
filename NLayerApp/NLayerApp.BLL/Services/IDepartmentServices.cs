using NLayerApp.BLL.DTO;

namespace NLayerApp.BLL.Services
{
    public interface IDepartmentServices
    {
        DepartmentsDTO Create(DepartmentsDTO department);
        DepartmentsDTO Update(int departmentId,DepartmentsDTO department);
        DepartmentsDTO GetById(int id);
        DepartmentsDTO GetByName(string name);
        bool Delete(int departmentId);
        ICollection<DepartmentsDTO> GetDepartments();
    }
}