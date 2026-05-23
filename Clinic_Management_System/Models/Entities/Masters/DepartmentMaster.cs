using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Entities.Masters
{
    [BsonIgnoreExtraElements]
    public class DepartmentMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DepartmentId { get; set; }
        public string ClinicId { get; set; }
        public string DepartmentName { get; set; }
        public int Status { get; set; }
    }
}
