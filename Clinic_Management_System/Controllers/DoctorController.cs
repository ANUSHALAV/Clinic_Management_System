using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Implementations;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("DoctorsByClinic")]
        public async Task<IActionResult> GetDoctrorsByClinicId(string ClinicId)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _doctorService.GetDoctorsByClinicId(ClinicId);
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Doctors retrieved successfully.";
                    res.TotalRecorde = Data.Count;
                }
                else
                {
                    res.Success = false;
                    res.Message = "No doctors found for the specified clinic.";
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

        [HttpGet]
        [Route("DoctorByDoctorIdAndClinicId")]
        public async Task<IActionResult> GetDoctrorsByDoctorIdAndClinicId(string UserId, string ClinicId)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _doctorService.GetDoctorsByDoctorIdAndClinicId(UserId, ClinicId);
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Doctor retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No doctor found for the specified clinic.";
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
