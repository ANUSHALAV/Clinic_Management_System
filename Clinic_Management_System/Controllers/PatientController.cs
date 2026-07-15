using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Route("Patients")]
        public async Task<IActionResult> GetPatientAsync([FromBody] ImportDTO obj)
        {
            var res = new APIResponse();
            try
            {
                var Patients = await _patientService.GetPatientAsync(obj);
                if (Patients != null)
                {
                    res.Success = true;
                    res.Data = Patients;
                    res.Message = "Patients retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No patient found.";
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
        [Route("PatientByClinicIdAndPatientId")]
        public async Task<IActionResult> GetPatientByClinicIdAndPatientIdAsync(string ClinicId, string PatientId)
        {
            var res = new APIResponse();
            try
            {
                var patient = await _patientService.GetPatientByClinicIdAndPatientIdAsync(ClinicId, PatientId);
                if (patient != null)
                {
                    res.Success = true;
                    res.Data = patient;
                    res.Message = "Patient retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No patient found with the provided Clinic ID and Patient ID.";
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
        [Route("AddPatient")]
        public async Task<IActionResult> AddPatientAsync([FromBody] AddPatientDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var result = await _patientService.AddPatientAsync(Obj);
                if (result != null)
                {
                    res.Data = result;
                    res.Success = true;
                    res.Message = "Patient added successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Failed to add patient.";
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
        [Route("UpdatePatientById")]
        public async Task<IActionResult> UpdatePatientDetailsByPatientId([FromBody] UpdatePatientDTO Obj)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _patientService.UpdatePatientDetailsByPatientId(Obj);
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Patient Details Retrived Successfully";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Patient Details Retrived Not Successfully";
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
