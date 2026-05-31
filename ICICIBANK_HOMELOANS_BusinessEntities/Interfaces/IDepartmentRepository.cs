using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetDepartments();
        public Task<Department> GetDepartmentById(int deptid);
        public Task<int> AddDepartment(Department department);
        public Task<int> UpdateDepartment(Department department);
        public Task<bool> DeleteDepartment(int deptid);
    }
}
