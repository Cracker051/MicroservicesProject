using Domain.Entities;
using MediatR;


namespace Application.Department.Queries.GetDepartmentById
{
    public class GetDepartmentByIdQuery:IRequest<Departments>
    {
        public int Id { get; set; }
    }
}
