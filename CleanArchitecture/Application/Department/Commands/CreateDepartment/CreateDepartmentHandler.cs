using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;


namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, Departments>
    {
        private readonly IApplicationContext _context;

        public CreateDepartmentHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Departments> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Departments()
            {
                Name = request.Name,
                Drugstores = new List<Drugstores>()
            };
            await _context.Department.AddAsync(department);
            await _context.SaveChangesAsync(cancellationToken);
            return department;
        }
    }
}