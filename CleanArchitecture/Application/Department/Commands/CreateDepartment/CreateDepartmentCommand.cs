using Domain.Entities;
using MediatR;

namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<Departments>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Drugstores> Drugstores { get; set; }
    }
}
