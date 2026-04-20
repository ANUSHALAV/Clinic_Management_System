using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Responses;

namespace Clinic_Management_System.Services.Interfaces
{
    public interface IPatientService
    {
        public Task<PatientResponse> GetPatientAsync(string ClinicId);

        public Task<PatientResponse> GetPatientByClinicIdAndPatientIdAsync(string ClinicId, string PatientId);

        public Task<AddPatientDTO> AddPatientAsync(AddPatientDTO Obj);

        public Task<UpdatePatientDTO> UpdatePatientDetailsByPatientId(UpdatePatientDTO Obj);
    }
}
