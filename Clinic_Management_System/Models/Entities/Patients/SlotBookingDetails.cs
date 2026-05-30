using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Entities.Patients
{
    [BsonIgnoreExtraElements]
    public class SlotBookingDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SlotBookingId { get; set; }
        public string ClinicId { get; set; }
        public string PatientId { get; set; }
        public string BookingNumber { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime AppointmentDate { get; set; }
        public string DepartmentId { get; set; }
        public string DoctorId { get; set; }
        public int Status { get; set; }

    }
}
