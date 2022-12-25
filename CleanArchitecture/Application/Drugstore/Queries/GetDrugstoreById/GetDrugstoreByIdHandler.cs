using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Drugstore.Queries.GetDrugstoreById
{
    public class GetDrugstoreByIdHandler:IRequestHandler<GetDepartmentByIdQuery, Drugstores>
    {
        private readonly IApplicationContext _context;

        public GetDrugstoreByIdHandler(IApplicationContext context)
        {
            _context = context;
        }
        public async Task<Drugstores> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            Drugstores entity = await _context.Drugstore.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Drugstores), request.Id);
            }
            return entity;
        }
    }
}
