using Domain.Entities;
using MediatR;

namespace Application.Drugstore.Commands.CreateDrugstore
{
    public class CreateDrugstoreCommand : IRequest<Drugstores>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public string Address { get; set; }
    }
}
