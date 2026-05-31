using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    var result = await _employeeService.AddEmployee(employeeDto);
                    return StatusCode(StatusCodes.Status201Created, "Employee added successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{empid}")]
        public async Task<IActionResult> DeleteEmployee(int empid)
        {
            try
            {
                var result = await _employeeService.DeleteEmployee(empid);
                if (result.Contains("does not exist"))
                {
                    return StatusCode(StatusCodes.Status404NotFound, "employee data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error");
            }

        }
        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public async Task<IActionResult> GetEmployeeById(int empid)
        {
            try
            {
                var result = await _employeeService.GetEmployeeById(empid);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "employee data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error");
            }
        }
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var result = await _employeeService.GetEmployees();
                if (result == null || result.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "employee data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    var result = await _employeeService.UpdateEmployee(employeeDto);
                    if (result.Contains("does not exist"))
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "employee data not found");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status200OK, "updated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error");
            }
        }
    }
}   