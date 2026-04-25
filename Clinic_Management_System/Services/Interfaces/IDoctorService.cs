using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Responses;

namespace Clinic_Management_System.Services.Interfaces
{
    public interface IDoctorService
    {
        public Task<List<DoctorResponse>> GetDoctorsByClinicIdAsync(string ClinicId);
        public Task<List<DoctorResponse>> GetDoctorsByDoctorIdAndClinicIdAsync(string UserId, string ClinicId);
        public Task<AddDoctorDTO> AddDoctorAsync(AddDoctorDTO Obj);
        public Task<UpdateDoctorDTO> UpdateDoctorAsync(UpdateDoctorDTO Obj);
    }
}
