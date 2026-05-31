using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentDto>> GetDepartments();
        public Task<DepartmentDto> GetDepartmentById(int deptid);
        public Task<int> AddDepartment(DepartmentDto departmentDto);
        public Task<int> UpdateDepartment(DepartmentDto departmentDto);
        public Task<bool> DeleteDepartment(int deptid);
    }
}
