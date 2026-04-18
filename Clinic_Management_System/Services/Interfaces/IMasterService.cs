using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Masters;
using Clinic_Management_System.Models.Masters;

namespace Clinic_Management_System.Services.Interfaces
{
    public interface IMasterService
    {
        public Task<List<UserTypeMaster>> GetUserTypeAsync();
        public Task<List<CountryMaster>> GetCountryAsync();

        public Task<List<StateMaster>> GetStateByCountryIdAsync(string CountryId);

        public Task<List<DistrictMaster>> GetDistrictByStateIdAsync(string StateId);

        public Task<List<ClinicMaster>> GetClinicAsync();

        public Task<ClinicDTO> AddClinicAsync(ClinicDTO ClinicDTOObj);
    }
}
