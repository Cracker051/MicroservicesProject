using Domain.Entities;
using MediatR;

namespace Application.Drugstore.Queries.GetDrugstoreById
{
    public class GetDepartmentByIdQuery:IRequest<Drugstores>
    {
        public int Id { get; set; }
    }
}
