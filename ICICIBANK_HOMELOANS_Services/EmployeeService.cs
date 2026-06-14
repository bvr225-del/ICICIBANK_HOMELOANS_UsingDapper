using AutoMapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _employeeRepository = employeeRepository;
            this._mapper = mapper;
            this._loggingFactory = loggingFactory;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddEmployee(EmployeeDto employeeDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeServices: AddEmployes method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: AddEmployee method execution started.");

            Employee emp =new Employee();
            _mapper.Map(employeeDto, emp);
            Log.Information("EmployeeService: AddEmployee method execution completed.");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: AddEmployee method execution completed.");

            return await _employeeRepository.AddEmployee(emp);



        }

        public async Task<string> DeleteEmployee(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeServices: DeleteEmployesById method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: DeleteEmployee method execution started.");

            var result = await _employeeRepository.DeleteEmployee(empid);
            Log.Information("EmployeeService: DeleteEmployee method execution completed.");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: DeleteEmployee method execution completed.");

            return result;

        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeServices: GetEmployeeById method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: GetEmployeeById method execution started.");

            var result = await _employeeRepository.GetEmployeeById(empid);
            Log.Information("EmployeeService: GetEmployeeById method execution completed.");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: GetEmployeeById method execution completed.");

            return _mapper.Map<EmployeeDto>(result);

        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeServices: GetEmployees method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: GetEmployees method execution started.");

            var result = await _employeeRepository.GetEmployees();
            Log.Information("EmployeeService: GetEmployees method execution completed.");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: GetEmployees method execution completed.");

            return _mapper.Map<List<EmployeeDto>>(result);

        }

        public async Task<string> UpdateEmployee(EmployeeDto employeeDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeServices: UpdateEmployee method Excution Starts and Current Loggedin username:{userName}");//logg the message in text file using serilog
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: UpdateEmployee method execution started.");

            Employee emp = new Employee();
            _mapper.Map(employeeDto, emp);
            var result = await _employeeRepository.UpdateEmployee(emp);
            Log.Information("EmployeeService: UpdateEmployee method execution completed.");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeService: UpdateEmployee method execution completed.");

            return result;
        }
    }
}
