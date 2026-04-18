using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Entities.Masters
{
    [BsonIgnoreExtraElements]
    public class ClinicMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClinicMasterId { get; set; }
        public string ClinicName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
    }
}
