using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;


namespace Application.Drugstore.Commands.CreateDrugstore
{
    public class CreateDrugstoreHandler : IRequestHandler<CreateDrugstoreCommand,Drugstores>
    {
        private readonly IApplicationContext _context;

        public CreateDrugstoreHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Drugstores> Handle(CreateDrugstoreCommand request, CancellationToken cancellationToken)
        {
            var drugstore = new Drugstores()
            {
                Name = request.Name,
                Address = request.Address,
                DepartmentId = request.DepartmentId
            };
            await _context.Drugstore.AddAsync(drugstore);
            await _context.SaveChangesAsync(cancellationToken);
            return drugstore;
        }
    }
}