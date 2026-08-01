using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Responses;

namespace Clinic_Management_System.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserResponse>> GetUsersByClinicIdAsync(ImportDTO obj);
        public Task<List<UserResponse>> GetUserByIdAndClinicIdAsync(string ClinicId, string UserId);
        public Task<AddUserDTO> AddUserAsync(AddUserDTO UserDTOObj);
        public Task<UpdateUserDTO> UpdateUserByUserIdAsync(UpdateUserDTO UpdateUserDTOObj);

    }
}
