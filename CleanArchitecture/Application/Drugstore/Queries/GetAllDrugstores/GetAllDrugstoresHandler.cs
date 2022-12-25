using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Drugstore.Queries.GetAllDrugstores
{
    public class GetAllDrugstoresHandler:IRequestHandler<GetAllDrugstoresQuery, List<Drugstores>>
    {
        private readonly IApplicationContext _context;

        public GetAllDrugstoresHandler(IApplicationContext context)
        {
            _context = context;
        }
        
        public async Task<List<Drugstores>> Handle(GetAllDrugstoresQuery request, CancellationToken cancellationToken)
        {
            List<Drugstores> drugstores = await _context.Drugstore.ToListAsync();
            return drugstores;
        }

    }
}
