using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IEmployeeRepository
    {
        public Task<int> AddEmployee(Employee employee);
        public Task<Employee> GetEmployeeById(int empid);
        public Task<List<Employee>> GetEmployees();
        public Task<string> UpdateEmployee(Employee employee);
        public Task<string> DeleteEmployee(int empid);
    }
}
