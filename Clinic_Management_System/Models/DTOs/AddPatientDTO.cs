using Clinic_Management_System.Models.Entities.Patients;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.DTOs
{
    public class AddPatientDTO
    {
        public string ClinicId { get; set; }
        public string UserTypeId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime AppointmentDate { get; set; }
        public string DoctorId { get; set; }
        public string DepartmentId { get; set; }
    }
}
