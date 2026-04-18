using Clinic_Management_System.Configurations;
using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Masters;
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

        public async Task<ClinicDTO> AddClinicAsync(ClinicDTO ClinicDTOObj)
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
    }
}
