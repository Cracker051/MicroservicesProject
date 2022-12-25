using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Department.Commands.UpdateDepartment
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, Departments>
    {
        private readonly IApplicationContext _context;

        public UpdateDepartmentHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Departments> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Departments department = await _context.Department.FindAsync(request.Id);

            if (department == null)
            {
                throw new NotFoundException(nameof(Departments), request.Id);
            }

            department.Name = request.Name.Trim();

            await _context.SaveChangesAsync(cancellationToken);

            return department;
        }
    }
}
