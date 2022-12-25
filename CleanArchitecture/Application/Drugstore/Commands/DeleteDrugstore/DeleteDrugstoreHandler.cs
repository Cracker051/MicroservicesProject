using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
namespace Application.Drugstore.Commands.DeleteDrugstore
{
    public class DeleteDrugstoreHandler:IRequestHandler<DeleteDrugstoreCommand,Drugstores>
    {
        private readonly IApplicationContext _context;

        public DeleteDrugstoreHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<Drugstores> Handle(DeleteDrugstoreCommand request, CancellationToken cancellationToken)
        {
            Drugstores drugstore = await _context.Drugstore.FindAsync(request.Id);
            if (drugstore == null)
            {
                throw new NotFoundException(nameof(Drugstores),request.Id);
            }
            _context.Drugstore.Remove(drugstore);
            await _context.SaveChangesAsync(cancellationToken);

            return drugstore;
        }
    }
}
