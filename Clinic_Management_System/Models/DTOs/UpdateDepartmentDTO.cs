namespace Clinic_Management_System.Models.DTOs
{
    public class UpdateDepartmentDTO
    {
        public string ClinicId { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Status { get; set; }
    }
}
