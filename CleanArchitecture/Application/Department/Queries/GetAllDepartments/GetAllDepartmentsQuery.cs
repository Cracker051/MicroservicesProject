using Domain.Entities;
using MediatR;


namespace Application.Department.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQuery : IRequest<List<Departments>>
    {
    }
}
