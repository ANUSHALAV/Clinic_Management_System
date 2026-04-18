using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clinic_Management_System.Models.Entities.Masters
{
    [BsonIgnoreExtraElements]
    public class UserTypeMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        public int Status { get; set; }
    }
}
