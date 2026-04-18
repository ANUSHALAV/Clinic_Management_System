using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Masters
{
    [BsonIgnoreExtraElements]
    public class CountryMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CountryId { get; set; }
        public string CountryName { get; set; }
        public int Status { get; set; }
    }
}
