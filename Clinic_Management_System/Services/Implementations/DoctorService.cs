using Clinic_Management_System.Common;
using Clinic_Management_System.Configurations;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace Clinic_Management_System.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IMongoDatabase _database;
        private readonly DBSettings _dbSettings;
        private readonly MongoClient _mongoClient;
        public DoctorService(DBSettings dbSettings) { 
            _dbSettings = dbSettings;
            _mongoClient = new MongoClient(_dbSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_dbSettings.DatabaseName);

        }

        public async Task<List<DoctorResponse>> GetDoctorsByClinicIdAsync(string ClinicId)
        {
            IMongoCollection<DoctorResponse> doctorCollection = _database.GetCollection<DoctorResponse>("User");

            var filter = Builders<DoctorResponse>.Filter.Where(d => d.ClinicId == ClinicId && d.Status == 1 && d.UserType == CommonVariables.Doctor);

            var result = await doctorCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<List<DoctorResponse>> GetDoctorsByDoctorIdAndClinicIdAsync(string UserId, string ClinicId)
        {
            IMongoCollection<DoctorResponse> doctorCollection = _database.GetCollection<DoctorResponse>("User");

            var filter = Builders<DoctorResponse>.Filter.Where(d => d.UserId == UserId && d.ClinicId == ClinicId && d.Status == 1 && d.UserType == CommonVariables.Doctor);

            var result = await doctorCollection.Find(filter).ToListAsync();

            return result;
        }

    }
}
