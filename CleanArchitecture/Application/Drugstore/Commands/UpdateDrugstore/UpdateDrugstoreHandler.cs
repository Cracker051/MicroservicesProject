using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Drugstore.Commands.UpdateDrugstore
{
    public class UpdateDrugstoreHandler : IRequestHandler<UpdateDrugstoreCommand, Drugstores>
    {
        private readonly IApplicationContext _context;

        public UpdateDrugstoreHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Drugstores> Handle(UpdateDrugstoreCommand request, CancellationToken cancellationToken)
        {
            Drugstores drugstore = await _context.Drugstore.FindAsync(request.Id);
            
            if (drugstore == null)
            {
                throw new NotFoundException(nameof(Drugstores), request.Id);
            }

            drugstore.Name = request.Name.Trim();
            drugstore.Address = request.Address.Trim();
            drugstore.DepartmentId = request.DepartmentId;

            await _context.SaveChangesAsync(cancellationToken);

            return drugstore;
        }
    }
}
