using Clinic_Management_System.Models.DTOs;
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
        public async Task<IActionResult> GetDoctrorsByClinicIdAsync(string ClinicId)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _doctorService.GetDoctorsByClinicIdAsync(ClinicId);
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
        public async Task<IActionResult> GetDoctrorsByDoctorIdAndClinicIdAsync(string UserId, string ClinicId)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _doctorService.GetDoctorsByDoctorIdAndClinicIdAsync(UserId, ClinicId);
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

        [HttpPost]
        [Route("AddDoctor")]
        public async Task<IActionResult> AddDoctorAsync([FromBody] AddDoctorDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var result = await _doctorService.AddDoctorAsync(Obj);
                if (result != null)
                {
                    res.Success = true;
                    res.Message = "Doctor Add Successfully";
                    res.Data = Obj;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Doctor not Added Successfully";
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

        [HttpPut]
        [Route("UpdateDoctor")]
        public async Task<IActionResult> UpdateDoctorAsync([FromBody] UpdateDoctorDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var result = await _doctorService.UpdateDoctorAsync(Obj);
                if (result != null)
                {
                    res.Success = true;
                    res.Message = "Doctor Update Successfully";
                    res.Data = Obj;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Doctor not Update Successfully";
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
