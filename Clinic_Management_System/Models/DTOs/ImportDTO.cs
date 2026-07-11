using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.CompilerServices;

namespace Clinic_Management_System.Models.DTOs
{
    public class ImportDTO
    {
        public string ClinicId { get; set; }
        public string UserId { get; set; }
        public string UserTypeId { get; set; }
        public string DepartmentId { get; set; }
        public List<SearchDTO> SearchList { get; set; }
        public int PageNumber { get; set; }
        public int DataLimit { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ToDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime FromDate { get; set; }
        public int Status { get; set; }
    }

    public class SearchDTO
    {
        public string SearchBy { get; set; }
        public string SearchValue { get; set; }
        public string[] SearchArray { get; set; }
    }
}
