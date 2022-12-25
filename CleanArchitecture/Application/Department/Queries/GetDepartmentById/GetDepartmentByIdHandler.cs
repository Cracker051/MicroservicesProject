using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Department.Queries.GetDepartmentById
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, Departments>
    {
        private readonly IApplicationContext _context;

        public GetDepartmentByIdHandler(IApplicationContext context)
        {
            _context = context;
        }
        public async Task<Departments> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Departments entity = await _context.Department.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Departments), request.Id);
            }
            return entity;
        }
    }
}
