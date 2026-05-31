using AutoMapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
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
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            this._mapper = mapper;
        }
        public async Task<int> AddEmployee(EmployeeDto employeeDto)
        {
            Employee emp=new Employee();
            _mapper.Map(employeeDto, emp);
            return await _employeeRepository.AddEmployee(emp);



        }

        public async Task<string> DeleteEmployee(int empid)
        {
            var result = await _employeeRepository.DeleteEmployee(empid);
            return result;

        }

        public async Task<EmployeeDto> GetEmployeeById(int empid)
        {
            var result = await _employeeRepository.GetEmployeeById(empid);
            return _mapper.Map<EmployeeDto>(result);

        }

        public async Task<List<EmployeeDto>> GetEmployees()
        {
            var result = await _employeeRepository.GetEmployees();
            return _mapper.Map<List<EmployeeDto>>(result);

        }

        public async Task<string> UpdateEmployee(EmployeeDto employeeDto)
        {
            Employee emp = new Employee();
            _mapper.Map(employeeDto, emp);
            var result = await _employeeRepository.UpdateEmployee(emp);
            return result;
        }
    }
}
