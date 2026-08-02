using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Responses;

namespace Clinic_Management_System.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<UserResponse> LoginAsync(LoginDTO Obj);

        public string GenerateToken(User user);
    }
}
