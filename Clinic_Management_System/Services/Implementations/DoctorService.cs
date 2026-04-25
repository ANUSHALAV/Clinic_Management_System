using Clinic_Management_System.Common;
using Clinic_Management_System.Configurations;
using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Clinic_Management_System.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IMongoDatabase _database;
        private readonly DBSettings _dbSettings;
        private readonly MongoClient _mongoClient;
        public DoctorService(DBSettings dbSettings)
        {
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

        public async Task<AddDoctorDTO> AddDoctorAsync(AddDoctorDTO Obj)
        {
            IMongoCollection<User> doctorCollection = _database.GetCollection<User>("User");

            var doctorDetail = new User
            {
                UserTypeId = CommonVariables.Doctor,
                FirstName = Obj.FirstName,
                LastName = Obj.LastName,
                Address = Obj.Address,
                Email = Obj.Email,
                DateOfBirth = Obj.DateOfBirth,
                Gender = Obj.FirstName,
                PhoneNumber = Obj.PhoneNumber,
                CountryId = Obj.CountryId,
                StateId = Obj.StateId,
                DistrictId = Obj.DistrictId,
                Status = 1
            };

            await doctorCollection.InsertOneAsync(doctorDetail);
            return Obj;
        }

        public async Task<UpdateDoctorDTO> UpdateDoctorAsync(UpdateDoctorDTO Obj)
        {
            IMongoCollection<User> doctorCollection = _database.GetCollection<User>("User");

            var updateUserDetail = Builders<User>.Filter.Where(u => u.UserId == Obj.UserId && u.Status == 1 && u.UserTypeId == CommonVariables.Doctor);

            if (updateUserDetail != null)
            {
                var UpdateFilter = Builders<User>.Update
                    .Set(u => u.FirstName, Obj.FirstName)
                    .Set(u => u.LastName, Obj.LastName)
                    .Set(u => u.Address, Obj.Address)
                    .Set(u => u.Email, Obj.Email)
                    .Set(u => u.PhoneNumber, Obj.PhoneNumber)
                    .Set(u => u.CountryId, Obj.CountryId)
                    .Set(u => u.StateId, Obj.StateId)
                    .Set(u => u.DistrictId, Obj.DistrictId)
                    .Set(u => u.Gender, Obj.Gender);

                var result = await doctorCollection.UpdateOneAsync(updateUserDetail, UpdateFilter);

                if (result.ModifiedCount > 0)
                {
                    return Obj;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

    }
}
