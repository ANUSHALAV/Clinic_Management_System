using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Responses
{
    public class UserResponse
    {
        public string ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string UserId { get; set; }
        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public string DistrictId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; } 
        public string Token { get; set; }
        public int Status { get; set; }

    }
}
