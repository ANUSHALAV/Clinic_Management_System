using Clinic_Management_System.Services.Interfaces;
using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Configurations;
using MongoDB.Driver;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Common;
using Microsoft.AspNetCore.Identity;

namespace Clinic_Management_System.Services.Implementations
{
    public class PatientService : IPatientService
    {
        private readonly DBSettings _dbSettings;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public PatientService(DBSettings dbSettings)
        {
            _dbSettings = dbSettings;
            _mongoClient = new MongoClient(_dbSettings.ConnectionString);
            _database = _mongoClient.GetDatabase(_dbSettings.DatabaseName);
        }

        public async Task<AddPatientDTO> AddPatientAsync(AddPatientDTO Obj)
        {
            IMongoCollection<User> patientCollection = _database.GetCollection<User>("User");
            var passwordHasher = new PasswordHasher<User>();
            var hashPassword = passwordHasher.HashPassword(null, Obj.Password); 
            var newPatient = new User
            {
                ClinicId = Obj.ClinicId,
                UserTypeId = CommonVariables.Patient,
                LoginId = Obj.LoginId,
                Password = hashPassword,
                FirstName = Obj.FirstName,
                LastName = Obj.LastName,
                DateOfBirth = Obj.DateOfBirth,
                Status = 1
            };
            await patientCollection.InsertOneAsync(newPatient);
            return Obj;
        }

        public async Task<PatientResponse> GetPatientAsync(string ClinicId)
        {
            IMongoCollection<User> patientCollection = _database.GetCollection<User>("User");

            var filter = Builders<User>.Filter.Where(p => p.Status == 1 && p.UserTypeId == CommonVariables.Patient && p.ClinicId == ClinicId);
            var patients = await patientCollection.Find(filter).FirstOrDefaultAsync();
            var result = new PatientResponse
            {
               FullName = patients.FirstName + " " + patients.LastName,
               PatientId = patients.UserId,
               ClinicId = patients.ClinicId,
               PatientName = patients.FirstName,
            };

            return result;
        }

        public async Task<PatientResponse> GetPatientByClinicIdAndPatientIdAsync(string ClinicId, string PatientId)
        {
            IMongoCollection<User> patientCollection = _database.GetCollection<User>("User");   

            var filter =  Builders<User>.Filter.Where(p=>p.Status==1&& p.UserTypeId==CommonVariables.Patient&& p.ClinicId == ClinicId && p.UserId == PatientId);

            var patient= await patientCollection.Find(filter).FirstOrDefaultAsync();

            var result = new PatientResponse
            {
                FullName = patient.FirstName + " " + patient.LastName,
                PatientId = patient.UserId,
                ClinicId = patient.ClinicId,
                PatientName = patient.FirstName,
            };  

            return result;
        }
    }
}
