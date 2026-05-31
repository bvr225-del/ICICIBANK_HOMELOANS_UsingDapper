using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IEmployeeService
    {
        public Task<int> AddEmployee(EmployeeDto employeeDto);
        public Task<EmployeeDto> GetEmployeeById(int empid);
        public Task<List<EmployeeDto>> GetEmployees();
        public Task<string> UpdateEmployee(EmployeeDto employeeDto);
        public Task<string> DeleteEmployee(int empid);
    }
}
