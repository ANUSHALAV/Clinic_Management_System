using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Masters
{
    [BsonIgnoreExtraElements]
    public class DistrictMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string StateId { get; set; }
        public int Status { get; set; }
    }
}
