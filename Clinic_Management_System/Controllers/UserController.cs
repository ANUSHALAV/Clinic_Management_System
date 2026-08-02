using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Users;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers.Users
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost,Authorize]
        [Route("UsersByClinic")]
        public async Task<IActionResult> GetUsersByClinicIdAsync([FromBody] ImportDTO obj)
        {
            var res = new APIResponse();
            try
            {
                var users = await _userService.GetUsersByClinicIdAsync(obj);
                if (users != null)
                {
                    res.Success = true;
                    res.Data = users;
                    res.Message = "User data retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No users found.";
                    return NotFound(res);
                }

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred white retrieving user data:{ex.Message}";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet,Authorize]
        [Route("UserByIdAndByClinic")]
        public async Task<IActionResult> GetUserByIdAndClinicIdAsync(string ClinicId, string UserId)
        {
            var res = new APIResponse();
            try
            {
                var UserData = await _userService.GetUserByIdAndClinicIdAsync(ClinicId, UserId);
                if (UserData != null)
                {
                    res.Data = UserData;
                    res.Success = true;
                    res.Message = "User data retrived successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "User data not retrived successfully.";
                    return NotFound(res);
                }

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost,Authorize]
        [Route("AddUser")]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var result = await _userService.AddUserAsync(Obj);
                res.Success = true;
                res.Data = Obj;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                res.Message = $"An error occurred white retrieving user data:{ex.Message}";
            }
            return Ok(res);
        }

        [HttpPut,Authorize]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUserByUserIdAsync([FromBody] UpdateUserDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _userService.UpdateUserByUserIdAsync(Obj);
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "User Update Successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "User not Update Successfully.";
                    return NotFound(res);
                }

            }
            catch (Exception ex)
            {
                res.Success = true;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
