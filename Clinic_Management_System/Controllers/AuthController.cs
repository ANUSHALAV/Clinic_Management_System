using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Implementations;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _authService.LoginAsync(Obj);
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Login Successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Login not Successfully.";
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


    }
}
