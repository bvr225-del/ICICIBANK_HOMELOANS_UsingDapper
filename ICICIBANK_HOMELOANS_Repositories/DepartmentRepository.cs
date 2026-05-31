using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public DepartmentRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddDepartment(Department department)
        {
            using(IDbConnection con=_connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p= new DynamicParameters();
                p.Add(StoredProcedureParameters.deptname,department.deptname);
                p.Add(StoredProcedureParameters.deptlocation, department.deptlocation);
                p.Add(StoredProcedureParameters.departmentInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteAsync(StoredProcedures.AddDepartment, p, commandType: CommandType.StoredProcedure);
                int insertedId = p.Get<int>(StoredProcedureParameters.departmentInsertedId);
                return insertedId;
            }
        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.deptid, deptid);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.DeleteDepartment, p, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }

        }

        public async Task<Department> GetDepartmentById(int deptid)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.deptid, deptid);
                var department = await con.QueryFirstOrDefaultAsync<Department>(StoredProcedures.GetDepartmentById, p, commandType: CommandType.StoredProcedure);
                return department;
            }

        }

        public async Task<List<Department>> GetDepartments()
        {
            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                var departments = await con.QueryAsync<Department>(StoredProcedures.GetDepartment, commandType: CommandType.StoredProcedure);
                return departments.ToList();
            }

        }

        public async Task<int> UpdateDepartment(Department department)
        {
            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.deptid, department.deptid);
                p.Add(StoredProcedureParameters.deptname, department.deptname);
                p.Add(StoredProcedureParameters.deptlocation, department.deptlocation);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.UpdateDepartment, p, commandType: CommandType.StoredProcedure);
                return rowsAffected;
            }

        }
    }
}
