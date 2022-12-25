using Domain.Entities;
using MediatR;

namespace Application.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand:IRequest<Departments>
    {
        public int Id { get; set; }
    }
}
