using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentController(IDepartmentService departmentService, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _departmentService = departmentService;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DepartmentDto departmentDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            #region serilog
            Log.Information($"DepartmentController: AddDepartment method Execution starts and Current Loggedin username:{userName}");
            Log.Information($"DepartmentController: AddDepartment method input parameter DepartmentName: {departmentDto.deptname}");
            Log.Information($"DepartmentController: AddDepartment method input parameter DepartmentLocation: {departmentDto.deptlocation}");
            #endregion
            #region database log
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: AddDepartment method Execution starts");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: AddDepartment method input parameter DepartmentName: {departmentDto.deptname}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: AddDepartment method input parameter DepartmentLocation: {departmentDto.deptlocation}");
            #endregion

            if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int insertedId = await _departmentService.AddDepartment(departmentDto);
                Log.Information("DepartmentController: AddDepartment method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: AddDepartment method Execution ended successfully");

                return StatusCode(StatusCodes.Status201Created, "created successfully");
                }
        }
        [HttpDelete]
        [Route("DeleteDepartment/{deptid}")]
        public async Task<IActionResult> DeleteDepartment(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"DepartmentController: DeleteDepartment method Execution starts and Current Loggedin username:{userName}");
            Log.Information($"DepartmentController: DeleteDepartment method input parameter DepartmentId: {deptid}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: DeleteDepartment method Execution starts");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: DeleteDepartment method input parameter DepartmentId: {deptid}");
            #endregion

            if (deptid < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid department id");

                }
                bool result = await _departmentService.DeleteDepartment(deptid);
                if (result)
                {
                Log.Information("DepartmentController: DeleteDepartment method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: DeleteDepartment method Execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department not found");
                }
        }
        [HttpGet]
        [Route("GetDepartmentById/{deptid}")]
        public async Task<IActionResult> GetDepartmentById(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"DepartmentController: GetDepartmentById method Execution starts and Current Loggedin username:{userName}");
            Log.Information($"DepartmentController: GetDepartmentById method input parameter DepartmentId: {deptid}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: GetDepartmentById method Execution starts");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: GetDepartmentById method input parameter DepartmentId: {deptid}");
            #endregion


            var department = await _departmentService.GetDepartmentById(deptid);
                if (department == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department not found");
                }
                else
                {
                Log.Information("DepartmentController: GetDepartmentById method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: GetDepartmentById method Execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, department);
                }
        }
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"DepartmentController: GetDepartments method Execution starts and Current Loggedin username:{userName}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: GetDepartments method Execution starts");
            #endregion

            var departments = await _departmentService.GetDepartments();
                if (departments == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department data not found");
                }
                else
                {
                Log.Information("DepartmentController: GetDepartments method Execution ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: GetDepartments method Execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, departments);
                }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"DepartmentController: UpdateDepartment method Execution starts and Current Loggedin username:{userName}");
            Log.Information($"DepartmentController: UpdateDepartment method input parameter DepartmentId: {departmentDto.deptid}");
            Log.Information($"DepartmentController: UpdateDepartment method input parameter DepartmentName: {departmentDto.deptname}");
            Log.Information($"DepartmentController: UpdateDepartment method input parameter DepartmentLocation: {departmentDto.deptlocation}");
            #endregion

            #region database log
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: UpdateDepartment method Execution starts");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: UpdateDepartment method input parameter DepartmentId: {departmentDto.deptid}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: UpdateDepartment method input parameter DepartmentName: {departmentDto.deptname}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentController: UpdateDepartment method input parameter DepartmentLocation: {departmentDto.deptlocation}");
            #endregion

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
            }
            else
            {
                int rowsAffected = await _departmentService.UpdateDepartment(departmentDto);
                if (rowsAffected > 0)
                {
                    Log.Information("DepartmentController: UpdateDepartment method Execution ended successfully");
                    await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentController: UpdateDepartment method Execution ended successfully");

                    return StatusCode(StatusCodes.Status200OK, "updated successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "department not found");
                }
            }
                    
        }
    }
}