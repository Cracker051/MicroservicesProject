using Domain.Common;
namespace Domain.Entities
{
    public class Drugstores:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Departments Department { get; set; }
        public int DepartmentId { get; set; }
        public string Address { get; set; }
    }
}
