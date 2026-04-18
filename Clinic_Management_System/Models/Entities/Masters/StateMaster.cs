using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Masters
{
    [BsonIgnoreExtraElements]
    public class StateMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CountryId { get; set; }
        public int Status { get; set; }
    }
}
