namespace NLayerApp.DAL.Entities
{
    public class Doctors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }

        public virtual ICollection<Patients> Patients { get; set; } = new List<Patients>();

    }
}
