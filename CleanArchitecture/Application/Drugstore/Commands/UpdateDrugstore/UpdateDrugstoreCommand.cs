using Domain.Entities;
using MediatR;

namespace Application.Drugstore.Commands.UpdateDrugstore
{
    public class UpdateDrugstoreCommand:IRequest<Drugstores>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
    }
}
