using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Responses;

namespace Clinic_Management_System.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<List<DoctorResponse>> GetDoctorsByClinicId(string ClinicId);
        public Task<DoctorResponse> GetDoctorsByDoctorIdAndClinicId(string UserId, string ClinicId);
        public Task<AddDoctorDTO> AddDoctorsByClinicId(AddDoctorDTO Obj);
    }
}
