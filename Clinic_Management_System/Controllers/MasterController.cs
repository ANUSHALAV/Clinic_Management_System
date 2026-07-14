using Clinic_Management_System.Models.DTOs;
using Clinic_Management_System.Models.Entities.Masters;
using Clinic_Management_System.Models.Responses;
using Clinic_Management_System.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers.Masters
{
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        public MasterController(IMasterService masterService)
        {
            this._masterService = masterService;
        }

        [HttpGet]
        [Route("UserType")]
        public async Task<IActionResult> GetUserTypeAsync()
        {
            var res = new APIResponse();
            try
            {
                var userType = await _masterService.GetUserTypeAsync();
                if (userType != null)
                {
                    res.Data = userType;
                    res.Success = true;
                    res.TotalRecorde = userType.Count;
                    res.Message = "User Type data retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "User Type data not retrieved successfully.";
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
        [Route("Country")]
        public async Task<IActionResult> GetCountryAsync()
        {
            var res = new APIResponse();
            try
            {
                var country = await this._masterService.GetCountryAsync();
                if (country != null)
                {
                    res.Success = true;
                    res.Data = country;
                    res.TotalRecorde = country.Count;
                    res.Message = "Country data retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No country data found.";
                    return NotFound(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred while retrieving country data: {ex.Message}";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("State")]
        public async Task<IActionResult> GetStateByCountryIdAsync(string CountryId)
        {
            var res = new APIResponse();
            try
            {
                var state = await _masterService.GetStateByCountryIdAsync(CountryId);
                if (state != null)
                {
                    res.Success = true;
                    res.Data = state;
                    res.TotalRecorde = state.Count;
                    res.Message = "State data retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No State data found.";
                    return NotFound(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred white retrieving state data:{ex.Message}";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("District")]
        public async Task<IActionResult> GetDistrictByStateIdAsync(string StateId)
        {
            var res = new APIResponse();
            try
            {
                var district = await _masterService.GetDistrictByStateIdAsync(StateId);
                if (district != null)
                {
                    res.Success = true;
                    res.Data = district;
                    res.TotalRecorde = district.Count;
                    res.Message = "District data retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "No State data found.";
                    return NotFound(res);
                }
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred white retrieving district data:{ex.Message}";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("Clinic")]
        public async Task<IActionResult> GetClinicAsync()
        {
            var res = new APIResponse();
            try
            {
                var clinic = await _masterService.GetClinicAsync();
                if (clinic != null)
                {
                    res.Success = true;
                    res.Data = clinic;
                    res.Message = "Clinic data retrieved successfully.";
                }
                else
                {
                    res.Success = false;
                    res.Message = "Clinic data retrieved successfully.";
                    return NotFound(res);
                }

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred white retrieving Clinic data:{ex.Message}";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPost]
        [Route("AddClinic")]
        public async Task<IActionResult> AddClinicAsync([FromBody] AddClinicDTO ClinicDTOObj)
        {
            var res = new APIResponse();
            try
            {
                await _masterService.AddClinicAsync(ClinicDTOObj);
                res.Success = true;
                res.Data = ClinicDTOObj;
                res.Message = "Clinic data save successfully.";

            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = $"An error occurred white saving Clinic data:{ex.Message}";
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPut]
        [Route("UpdateClinic")]
        public async Task<IActionResult> UpdateClinicAsync([FromBody] UpdateClinicDTO UpdateClinicDTOObj)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _masterService.UpdateClinicAsync(UpdateClinicDTOObj);
                if (Data != null)
                {
                    res.Success = true;
                    res.Message = "Clinic Update Succesfully";
                    res.Data = Data;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Clinic not Update Succesfully";
                    return NotFound(res);
                }
                    
            }catch(Exception ex)
            {
                res.Success = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("Depeartment")]
        public async Task<IActionResult> GetDepartmentAsync()
        {
            var res = new APIResponse();
            try
            {
                var Data = await _masterService.GetDepartmentAsync();
                if (Data != null)
                {
                    res.Data = Data;
                    res.Success = true;
                    res.Message = "Deparment Reterived Successfull.";
                    res.TotalRecorde = Data.Count;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Deparment not Reterived Successfull.";
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
        [Route("AddDepratment")]
        public async Task<IActionResult> AddDepartmentAsync([FromBody] AddDepartmentDTO DepartmentDTOObj)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _masterService.AddDepartmentAsync(DepartmentDTOObj);
                if (Data != null)
                {
                    res.Success = true;
                    res.Message = "Deparment Add Successfully.";
                    res.Data = Data;
                }else
                {
                    res.Success = false;
                    res.Message = "Deparment not Add Successfully.";
                    return NotFound(res);
                }

            }catch(Exception ex)
            {
                res.Success = true;
                res.Message = ex.Message;
                return BadRequest(res);
            }
            return Ok(res);
        }

        [HttpPut]
        [Route("UpdateDepratment")]
        public async Task<IActionResult> UpdateepartmentAsync([FromBody] UpdateDepartmentDTO DepartmentDTOObj)
        {
            var res = new APIResponse();
            try
            {
                var Data = await _masterService.UpdateDepartmentAsync(DepartmentDTOObj);
                if (Data != null)
                {
                    res.Success = true;
                    res.Message = "Deparment Update Successfully.";
                    res.Data = Data;
                }
                else
                {
                    res.Success = false;
                    res.Message = "Deparment not Update Successfully.";
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
