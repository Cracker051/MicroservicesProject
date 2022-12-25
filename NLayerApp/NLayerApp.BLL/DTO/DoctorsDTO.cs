
namespace NLayerApp.BLL.DTO
{
    public class DoctorsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }

        public ICollection<PatientsDTO> Patients { get; set; } = new List<PatientsDTO>();
    }
}
