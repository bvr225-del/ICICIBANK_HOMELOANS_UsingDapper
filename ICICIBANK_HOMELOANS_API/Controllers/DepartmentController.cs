using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentDto departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int insertedId = await _departmentService.AddDepartment(departmentDto);
                    return StatusCode(StatusCodes.Status201Created, "created successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpDelete]
        [Route("DeleteDepartment/{deptid}")]
        public async Task<IActionResult> DeleteDepartment(int deptid)
        {
            try
            {
                if (deptid < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid department id");

                }
                bool result = await _departmentService.DeleteDepartment(deptid);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpGet]
        [Route("GetDepartmentById/{deptid}")]
        public async Task<IActionResult> GetDepartmentById(int deptid)
        {
            try
            {
                var department = await _departmentService.GetDepartmentById(deptid);
                if (department == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, department);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var departments = await _departmentService.GetDepartments();
                if (departments == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department data not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, departments);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int rowsAffected = await _departmentService.UpdateDepartment(departmentDto);
                    if (rowsAffected > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, "updated successfully");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "department not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
    }
}