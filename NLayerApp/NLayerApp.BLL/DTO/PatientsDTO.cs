namespace NLayerApp.BLL.DTO
{
    public class PatientsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Diagnosis { get; set; }
        public int DoctorId { get; set; }
        public DateOnly Arrive_date { get; set; }
        public DateOnly? Recovery_date { get; set; }
    }
}
