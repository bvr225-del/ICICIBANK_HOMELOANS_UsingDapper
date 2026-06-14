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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, ILoggingFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _departmentRepository = departmentRepository;
            this._mapper = mapper;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddDepartment(DepartmentDto departmentDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentService:AddDepartment method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:AddDepartment method execution started");

            Department dept =new Department();
            _mapper.Map(departmentDto, dept);
            Log.Information("DepartmentService:AddDepartment method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:AddDepartment method execution completed");

            return await _departmentRepository.AddDepartment(dept);
        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentService: DeleteDepartment method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:DeleteDepartment method execution started");

            var result = await _departmentRepository.DeleteDepartment(deptid);
            Log.Information("DepartmentService:DeleteDepartment method execution ended");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:DeleteDepartment method execution ended");

            return result;
        }

        public async Task<DepartmentDto> GetDepartmentById(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentService:GetDepartmentById method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:GetDepartmentById method execution started");

            var res = await _departmentRepository.GetDepartmentById(deptid);
            Log.Information("DepartmentService:GetDepartmentById method execution ended");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:GetDepartmentById method execution ended");


            return _mapper.Map<DepartmentDto>(res);
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentService:GetDepartment method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:GetDepartment method execution started");

            var res = await _departmentRepository.GetDepartments();
            Log.Information("DepartmentService:GetDepartment method execution ended");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:GetDepartment method execution ended");

            return _mapper.Map<List<DepartmentDto>>(res);
        }

        public async Task<int> UpdateDepartment(DepartmentDto departmentDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentService:UpdateDepartment method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:UpdateDepartment method execution started");

            Department dept = new Department();
            _mapper.Map(departmentDto, dept);
            var result = await _departmentRepository.UpdateDepartment(dept);
            Log.Information("DepartmentService:UpdateDepartment method execution ended");
            await _loggerFactory.AddLoggingMessages(userName, "information", "DepartmentService:UpdateDepartment method execution ended");

            return result;
        }
    }
}
