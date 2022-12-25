using Domain.Entities;
using MediatR;


namespace Application.Drugstore.Queries.GetAllDrugstores
{
    public class GetAllDrugstoresQuery:IRequest<List<Drugstores>>
    {
    }
}
