using Domain.Entities;
using MediatR;

namespace Application.Drugstore.Commands.DeleteDrugstore
{
    public class DeleteDrugstoreCommand:IRequest<Drugstores>
    {
        public int Id { get; set; }
    }
}
