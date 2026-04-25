using Clinic_Management_System.Configurations;
using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Masters;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Masters;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Clinic_Management_System.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DBSettings _dbSetting;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        public UserService(DBSettings dbSetting)
        {
            _dbSetting = dbSetting;
            _mongoClient = new MongoClient(_dbSetting.ConnectionString);
            _database = _mongoClient.GetDatabase(_dbSetting.DatabaseName);
        }

        public async Task<List<UserResponse>> GetUsersByClinicIdAsync(string ClinicId)
        {
            var clinicCollection = _database.GetCollection<ClinicMaster>("ClinicMaster");
            var userTypeCollection = _database.GetCollection<UserTypeMaster>("UserTypeMaster");
            var userCollection = _database.GetCollection<User>("User");
            var countryCollection = _database.GetCollection<CountryMaster>("CountryMaster");
            var stateCollection = _database.GetCollection<StateMaster>("StateMaster");
            var districtCollection = _database.GetCollection<DistrictMaster>("DistrictMaster");

            var clinics = await clinicCollection.Find(c => c.Status == 1).ToListAsync();
            var userTypes = await userTypeCollection.Find(ut => ut.Status == 1).ToListAsync();
            var users = await userCollection.Find(u => u.Status == 1).ToListAsync();
            var country = await countryCollection.Find(w => w.Status == 1).ToListAsync();
            var state = await stateCollection.Find(w => w.Status == 1).ToListAsync();
            var district = await districtCollection.Find(w => w.Status == 1).ToListAsync();

            var query = (from u in users

                         join cli in clinics on u.ClinicId equals cli.ClinicMasterId into uclinic
                         from cli in uclinic.DefaultIfEmpty()

                         join ut in userTypes on u.UserTypeId equals ut.UserTypeId into usertypes
                         from ut in usertypes.DefaultIfEmpty()

                         join c in country on u.CountryId equals c.CountryId into uc
                         from c in uc.DefaultIfEmpty()

                         join s in state on u.StateId equals s.StateId into us
                         from s in us.DefaultIfEmpty()

                         join d in district on u.DistrictId equals d.DistrictId into ud
                         from d in ud.DefaultIfEmpty()

                         select new UserResponse
                         {
                             ClinicId = cli.ClinicMasterId,
                             ClinicName = cli.ClinicName,
                             UserTypeId = ut.UserTypeId,
                             UserType = ut.UserType,
                             UserId = u.UserId,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             DateOfBirth = u.DateOfBirth,
                             Email = u.Email,
                             Address = u.Address,
                             PhoneNumber = u.PhoneNumber,
                             Gender = u.Gender,
                             CountryId = u.CountryId,
                             StateId = u.StateId,
                             DistrictId = u.DistrictId,
                             CountryName = c.CountryName,
                             StateName = s.StateName,
                             DistrictName = d.DistrictName,
                             Status = u.Status
                         }).ToList();

            return query;
        }

        public async Task<List<UserResponse>> GetUserByIdAndClinicIdAsync(string ClinicId, string UserId)
        {
            IMongoCollection<User> userCollection = _database.GetCollection<User>("User");
            IMongoCollection<CountryMaster> countryCollection = _database.GetCollection<CountryMaster>("CountryMaster");
            IMongoCollection<StateMaster> stateCollection = _database.GetCollection<StateMaster>("StateMaster");
            IMongoCollection<DistrictMaster> districtCollection = _database.GetCollection<DistrictMaster>("DistrictMaster");
            IMongoCollection<ClinicMaster> clinicCollection = _database.GetCollection<ClinicMaster>("ClinicMaster");
            IMongoCollection<UserTypeMaster> userTypeMasterCollection = _database.GetCollection<UserTypeMaster>("UserTypeMaster");

            if (UserId != null && ClinicId != null)
            {
                var filterUserData = await userCollection.Find(u => u.ClinicId == ClinicId && u.UserId == UserId).FirstOrDefaultAsync();

                if (filterUserData != null)
                {
                    var clinic = await clinicCollection.Find(c => c.ClinicMasterId == filterUserData.ClinicId && c.Status == 1).FirstOrDefaultAsync();
                    var country = await countryCollection.Find(w => w.CountryId == filterUserData.CountryId && w.Status == 1).FirstOrDefaultAsync();
                    var state = await stateCollection.Find(w => w.StateId == filterUserData.StateId && w.Status == 1).FirstOrDefaultAsync();
                    var district = await districtCollection.Find(w => w.DistrictId == filterUserData.DistrictId && w.Status == 1).FirstOrDefaultAsync();
                    var userType = await userTypeMasterCollection.Find(w => w.UserTypeId == filterUserData.UserTypeId && w.Status == 1).FirstOrDefaultAsync();

                    return new List<UserResponse>
                    {
                        new UserResponse
                        {
                        UserId = filterUserData.UserId,
                        UserTypeId = filterUserData.UserTypeId,
                        UserType = userType.UserType,
                        ClinicId  = clinic.ClinicMasterId,
                        ClinicName = clinic.ClinicName,
                        FirstName = filterUserData.FirstName,
                        LastName = filterUserData.LastName,
                        DateOfBirth = filterUserData.DateOfBirth,
                        Email = filterUserData.Email,
                        Address = filterUserData.Address,
                        Gender = filterUserData.Gender,
                        PhoneNumber = filterUserData.PhoneNumber,
                        CountryId = filterUserData.CountryId,
                        StateId = filterUserData.StateId,
                        DistrictId = filterUserData.DistrictId,
                        CountryName = country.CountryName,
                        StateName = state.StateName,
                        DistrictName = district.DistrictName,
                        Status = filterUserData.Status
                       }
                    };
                }
            }
            return new List<UserResponse>();

        }

        public async Task<AddUserDTO> AddUserAsync(AddUserDTO UserDTOObj)
        {
            IMongoCollection<User> userCollection = _database.GetCollection<User>("User");

            var passwordHasher = new PasswordHasher<User>();
            var hashPassword = passwordHasher.HashPassword(null, UserDTOObj.Password);
            var status = 1;

            var newUser = new User
            {
                ClinicId = UserDTOObj.ClinicId,
                UserTypeId = UserDTOObj.UserTypeId,
                FirstName = UserDTOObj.FirstName,
                LastName = UserDTOObj.LastName,
                DateOfBirth = UserDTOObj.DateOfBirth,
                Email = UserDTOObj.Email,
                Address = UserDTOObj.Address,
                Gender = UserDTOObj.Gender,
                PhoneNumber = UserDTOObj.PhoneNumber,
                LoginId = UserDTOObj.LoginId,
                CountryId = UserDTOObj.CountryId,
                StateId = UserDTOObj.StateId,
                DistrictId = UserDTOObj.DistrictId,
                Password = hashPassword,
                Status = status
            };

            await userCollection.InsertOneAsync(newUser);
            return UserDTOObj;
        }

        public async Task<UpdateUserDTO> UpdateUserByUserIdAsync(UpdateUserDTO UpdateUserDTOObj)
        {
            if (UpdateUserDTOObj != null)
            {
                IMongoCollection<User> userCollection = _database.GetCollection<User>("User");

                var filterUser = Builders<User>.Filter.Where(u => u.UserId == UpdateUserDTOObj.UserId && u.Status == 1);
                if (filterUser != null)
                {
                    var update = Builders<User>.Update
                         .Set(u => u.FirstName, UpdateUserDTOObj.FirstName)
                         .Set(u => u.LastName, UpdateUserDTOObj.LastName)
                         .Set(u => u.Address, UpdateUserDTOObj.Address)
                         .Set(u => u.Email, UpdateUserDTOObj.Email)
                         .Set(u => u.PhoneNumber, UpdateUserDTOObj.PhoneNumber)
                         .Set(u => u.Gender, UpdateUserDTOObj.Gender)
                         .Set(u => u.CountryId, UpdateUserDTOObj.CountryId)
                         .Set(u => u.StateId, UpdateUserDTOObj.StateId);

                    var result = await userCollection.UpdateOneAsync(filterUser, update);

                    if (result.ModifiedCount > 0)
                    {
                        return UpdateUserDTOObj;
                    }
                }
            }
            return null;
        }

        public async Task<UserResponse> LoginAsync([FromBody] LoginDTO Obj)
        {
            if (Obj != null)
            {
                IMongoCollection<User> UserCollection = _database.GetCollection<User>("User");

                var filterUser = Builders<User>.Filter.Where(u=>u.LoginId==Obj.LoginId && u.Status == 1);
                var userData = await UserCollection.Find(filterUser).FirstOrDefaultAsync();
                UserResponse response;
                if (userData != null)
                {
                    var passwordHasher = new PasswordHasher<User>();
                    var isPasswordValid = passwordHasher.VerifyHashedPassword(null, userData.Password, Obj.Password);

                    if (isPasswordValid == PasswordVerificationResult.Success)
                    {
                        response =  new UserResponse
                        {
                            UserId = userData.UserId,
                            UserTypeId = userData.UserTypeId,
                            ClinicId = userData.ClinicId,
                            FirstName = userData.FirstName,
                            LastName = userData.LastName,
                            DateOfBirth = userData.DateOfBirth,
                            Email = userData.Email,
                            Address = userData.Address,
                            Gender = userData.Gender,
                            PhoneNumber = userData.PhoneNumber,
                        };

                        return response;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            return null;
        }
    }
}
