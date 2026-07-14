using Clinic_Management_System.Configurations;
using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Masters;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Masters;
using Clinic_Management_System.Services.Interfaces;
using MongoDB.Driver;

namespace Clinic_Management_System.Services.Implementations
{
    public class MasterService : IMasterService
    {
        private readonly DBSettings _dbSetting;
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MasterService(DBSettings dbSettings)
        {
            this._dbSetting = dbSettings;
            this._mongoClient = new MongoClient(_dbSetting.ConnectionString);
            this._database = _mongoClient.GetDatabase(_dbSetting.DatabaseName);

        }

        public async Task<List<UserTypeMaster>> GetUserTypeAsync()
        {
            IMongoCollection<UserTypeMaster> userTypeMasterCollection = _database.GetCollection<UserTypeMaster>("UserTypeMaster");

            var userTypeData = await userTypeMasterCollection.Find(ut => ut.Status == 1).ToListAsync();

            return userTypeData;
        }

        public async Task<List<CountryMaster>> GetCountryAsync()
        {
            var countryCollection = _database.GetCollection<CountryMaster>("CountryMaster");

            var countryList = await countryCollection.Find(c => c.Status == 1).ToListAsync();

            return countryList;
        }

        public async Task<List<StateMaster>> GetStateByCountryIdAsync(string CountryId)
        {
            var stateCollection = _database.GetCollection<StateMaster>("StateMaster");

            var stateList = await stateCollection.Find(s => s.CountryId == CountryId && s.Status == 1).ToListAsync();

            return stateList;
        }

        public async Task<List<DistrictMaster>> GetDistrictByStateIdAsync(string StateId)
        {
            var districtCollection = _database.GetCollection<DistrictMaster>("DistrictMaster");

            var district = await districtCollection.Find(w => w.StateId == StateId && w.Status == 1).ToListAsync();

            return district;
        }

        public async Task<List<ClinicMaster>> GetClinicAsync()
        {
            IMongoCollection<ClinicMaster> clinicMasterCollection = _database.GetCollection<ClinicMaster>("ClinicMaster");

            var clinic = await clinicMasterCollection.Find(c => c.Status == 1).ToListAsync();

            return clinic;
        }

        public async Task<AddClinicDTO> AddClinicAsync(AddClinicDTO ClinicDTOObj)
        {
            IMongoCollection<ClinicMaster> clinicCollection = _database.GetCollection<ClinicMaster>("ClinicMaster");

            var clinicData = new ClinicMaster
            {
                ClinicName = ClinicDTOObj.ClinicName,
                Address = ClinicDTOObj.Address,
                Email = ClinicDTOObj.Email,
                Phone = ClinicDTOObj.Phone,
                Status = 1
            };
            await clinicCollection.InsertOneAsync(clinicData);
            return ClinicDTOObj;
        }

        public async Task<UpdateClinicDTO> UpdateClinicAsync(UpdateClinicDTO UpdateClinicDTOObj)
        {
            IMongoCollection<ClinicMaster> ClinicMasterCollection = _database.GetCollection<ClinicMaster>("ClinicMaster");

            var filter = Builders<ClinicMaster>.Filter.Where(c => c.ClinicId == UpdateClinicDTOObj.ClinicId&&c.Status==1);

            if (filter != null)
            {
                var updateClinic = Builders<ClinicMaster>.Update
                    .Set(c => c.ClinicId, UpdateClinicDTOObj.ClinicId)
                    .Set(c => c.ClinicName, UpdateClinicDTOObj.ClinicName)
                    .Set(c => c.Phone, UpdateClinicDTOObj.Phone)
                    .Set(c => c.Email, UpdateClinicDTOObj.Email)
                    .Set(c => c.Address, UpdateClinicDTOObj.Address)
                    .Set(c => c.Status, UpdateClinicDTOObj.Status);

                var result = await ClinicMasterCollection.UpdateOneAsync(filter, updateClinic);

                if (result.ModifiedCount > 0)
                {
                    return UpdateClinicDTOObj;
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

        public async Task<List<DepartmentMaster>> GetDepartmentAsync()
        {
            IMongoCollection<DepartmentMaster> DeparmentMasterCollection = _database.GetCollection<DepartmentMaster>("DepartmentMaster");
            var deparment = await DeparmentMasterCollection.Find(d => d.Status == 1).ToListAsync();
            return deparment;
        }

        public async Task<AddDepartmentDTO> AddDepartmentAsync(AddDepartmentDTO DepartmentDTOObj)
        {
            IMongoCollection<DepartmentMaster> DeparmentMasterCollection = _database.GetCollection<DepartmentMaster>("DepartmentMaster");
            if (DepartmentDTOObj != null)
            {
                var departmentData = new DepartmentMaster
                {
                    ClinicId = DepartmentDTOObj.ClinicId,
                    DepartmentName = DepartmentDTOObj.DepartmentName,
                    Status = 1
                };

                await DeparmentMasterCollection.InsertOneAsync(departmentData);
            }
            else
            {
                return null;
            }
            return DepartmentDTOObj;
        }

        public async Task<UpdateDepartmentDTO> UpdateDepartmentAsync(UpdateDepartmentDTO DepartmentDTOObj)
        {
            IMongoCollection<DepartmentMaster> DeparmentMasterCollection = _database.GetCollection<DepartmentMaster>("DepartmentMaster");
            var filter = Builders<DepartmentMaster>.Filter.Where(d => d.ClinicId == DepartmentDTOObj.ClinicId && d.DepartmentId == DepartmentDTOObj.DepartmentId && d.Status == 1);
            if (filter != null)
            {
                var updateDepeartment = Builders<DepartmentMaster>.Update
                        .Set(u => u.ClinicId, DepartmentDTOObj.ClinicId)
                        .Set(u => u.DepartmentName, DepartmentDTOObj.DepartmentName)
                        .Set(u => u.Status, DepartmentDTOObj.Status);

                var result = await DeparmentMasterCollection.UpdateOneAsync(filter, updateDepeartment);
                if (result.ModifiedCount > 0)
                {
                    return DepartmentDTOObj;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
