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
    public class DepartmentService : IDepartmentService
    {
            private readonly IDepartmentRepository _departmentRepository;
             private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            this._mapper = mapper;
        }

        public async Task<int> AddDepartment(DepartmentDto departmentDto)
        {
            Department dept=new Department();
            _mapper.Map(departmentDto, dept);
            return await _departmentRepository.AddDepartment(dept);
        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
            var result = await _departmentRepository.DeleteDepartment(deptid);
            return result;
        }

        public async Task<DepartmentDto> GetDepartmentById(int deptid)
        {
            var res= await _departmentRepository.GetDepartmentById(deptid);
            return _mapper.Map<DepartmentDto>(res);
        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            var res = await _departmentRepository.GetDepartments();
            return _mapper.Map<List<DepartmentDto>>(res);
        }

        public async Task<int> UpdateDepartment(DepartmentDto departmentDto)
        {
            Department dept = new Department();
            _mapper.Map(departmentDto, dept);
            var result = await _departmentRepository.UpdateDepartment(dept);
            return result;
        }
    }
}
