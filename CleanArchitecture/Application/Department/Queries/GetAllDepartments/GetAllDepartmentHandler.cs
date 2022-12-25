using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Department.Queries.GetAllDepartments
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<Departments>>
    {
        private readonly IApplicationContext _context;

        public GetAllDepartmentsHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Departments>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            List<Departments> departments = await _context.Department.ToListAsync();
            return departments;
        }

    }
}
