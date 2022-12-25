using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, Departments>
    {
        private readonly IApplicationContext _context;

        public DeleteDepartmentHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Departments> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            Departments department = await _context.Department.FindAsync(request.Id);
            if (department == null)
            {
                throw new NotFoundException(nameof(Departments), request.Id);
            }
            _context.Department.Remove(department);
            await _context.SaveChangesAsync(cancellationToken);
            return department;
        }
    }
}
