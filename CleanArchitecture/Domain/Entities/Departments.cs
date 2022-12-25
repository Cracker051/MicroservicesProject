using Domain.Common;
namespace Domain.Entities
{
    public class Departments:BaseEntity
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<Drugstores> Drugstores { get; set; }
    }
}
