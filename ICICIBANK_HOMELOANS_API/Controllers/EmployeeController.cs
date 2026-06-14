using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.IdentityModel.Tokens.Jwt;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeController(IEmployeeService employeeService, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _employeeService = employeeService;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeDto employeeDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region Serilog
            Log.Information($"EmployeeController: Post Api method Excution Starts and Current Loggedin username:{userName}");
            Log.Information($"EmployeeController:input parameter EmployeeName: {employeeDto.empname}");
            Log.Information($"EmployeeController:input parameter EmployeeSalary: {employeeDto.empsalary}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:Employee PosT API method Execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:input parameter EmployeeName: {employeeDto.empname}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:input parameter EmployeeSalary: {employeeDto.empsalary}");
            #endregion

            //throw new Exception("Custom Exception:Employee Controller:Post API method failed");

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
            }
            else
            {
                var result = await _employeeService.AddEmployee(employeeDto);
                Log.Information("EmployeeController:Employee PosT API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeController:Employee PosT API method Execution completed successfully");

                return StatusCode(StatusCodes.Status201Created, "Employee added successfully");
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{empid}")]
        public async Task<IActionResult> DeleteEmployee(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: delete Api method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            #region databaselog
            await _loggingFactory.AddLoggingMessages(userName, "information", $"Employee Controller:Delete API method Execution Started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            var result = await _employeeService.DeleteEmployee(empid);
            if (result.Contains("does not exist"))
            {
                return StatusCode(StatusCodes.Status404NotFound, "employee data not found");
            }
            else
            {
                Log.Information("Employee Controller:Delete API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Employee Controller:Delete API method Execution completed successfully");
                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
            }

        }
        [HttpGet]
        [Route("GetEmployeeById/{empid}")]
        public async Task<IActionResult> GetEmployeeById(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: Get Api method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

            #region databaselog
            await _loggingFactory.AddLoggingMessages(userName, "information", $"Employee Controller:GetEmployeeById API method Execution Started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:passed Input Parameter EmployeeId:{empid}");
            #endregion

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
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            //To read  the token from postman/react/angular/mobile application we used below code 
            //===================First way of read token properties(Just understanding purpose)=================================================
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authHeader.Replace("Bearer ", "");


            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(token);
            Dictionary<string, string> obj = new Dictionary<string, string>();
            foreach (var claim in jwtToken.Claims)
            {
                obj.Add(claim.Type, claim.Value);
            }

            var totaldata = obj;

            //====================Second way of read token properties(Just  understanding purpose)===================
            var userName1 = User.FindFirst("UserName")?.Value;

            var email = User.FindFirst("EmailId")?.Value;

            var phone = User.FindFirst("PhoneNumber")?.Value;

            var address = User.FindFirst("Address")?.Value;

            var isActive = User.FindFirst("IsActive")?.Value;

            var role = User.FindFirst("Roles")?.Value;
            //=================================
            //here read the username from token and this username used for logging purpose.
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: GetEmployees Api method Excution Starts and Current Loggedin username:{userName}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages(userName, "information", $"Employee Controller:GetEmployees API method Execution Started and Current Loggedin username:{userName}");
            #endregion

            //throw new Exception("Custom Exception:Employee Controller:GetEmployees API method failed");

            var result = await _employeeService.GetEmployees();
            if (result == null || result.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "employee data not found");
            }
            else
            {
                Log.Information("Employee Controller:GetEmployees API method Execution completed successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Employee Controller:GetEmployees API method Execution completed successfully");

                return StatusCode(StatusCodes.Status200OK, result);
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeDto employeeDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"EmployeeController: put Api method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            Log.Information($"EmployeeController:input parameter EmployeeId: {employeeDto.empid}");
            Log.Information($"EmployeeController:input parameter EmployeeName: {employeeDto.empname}");
            Log.Information($"EmployeeController:input parameter EmployeeSalary: {employeeDto.empsalary}");
            #endregion
            #region databaselog
            await _loggingFactory.AddLoggingMessages(userName, "information", $"Employee Controller:UpdateEmployee API method Execution Started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:input parameter EmployeeId: {employeeDto.empid}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:input parameter EmployeeName: {employeeDto.empname}");
            await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeController:input parameter EmployeeSalary: {employeeDto.empsalary}");
            #endregion

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
    }
}