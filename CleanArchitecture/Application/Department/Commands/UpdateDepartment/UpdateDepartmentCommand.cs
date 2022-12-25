using Domain.Entities;
using MediatR;

namespace Application.Department.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand:IRequest<Departments>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
