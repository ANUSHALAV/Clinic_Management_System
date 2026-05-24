namespace Clinic_Management_System.Models.DTOs
{
    public class UpdateClinicDTO
    {
        public string ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Status { get; set; };
    }
}
