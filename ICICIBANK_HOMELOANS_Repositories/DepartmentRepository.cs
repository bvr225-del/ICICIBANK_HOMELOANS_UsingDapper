using Dapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddDepartment(Department department)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: AddDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: AddDepartment method execution started");

            using (IDbConnection con=_connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p= new DynamicParameters();
                p.Add(StoredProcedureParameters.deptname,department.deptname);
                p.Add(StoredProcedureParameters.deptlocation, department.deptlocation);
                p.Add(StoredProcedureParameters.departmentInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteAsync(StoredProcedures.AddDepartment, p, commandType: CommandType.StoredProcedure);
                int insertedId = p.Get<int>(StoredProcedureParameters.departmentInsertedId);
                Log.Information("DepartmentRepository: AddDepartment method execution completed");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: AddDepartment method execution completed");

                Log.Information($"DepartmentRepository: AddDepartment method execution completed with DepartmentInsert value: {insertedId}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: AddDepartment method execution completed with DepartmentInsert value: {insertedId}");

                return insertedId;
            }
        }

        public async Task<bool> DeleteDepartment(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: DeleteDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: DeleteDepartment method execution started");

            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.deptid, deptid);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.DeleteDepartment, p, commandType: CommandType.StoredProcedure);
                Log.Information($"DepartmentRepository: DeleteDepartment method execution completed with DepartmentId: {deptid}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: DeleteDepartment method execution completed with DepartmentId: {deptid}");

                return rowsAffected > 0;
            }

        }

        public async Task<Department> GetDepartmentById(int deptid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: GetDepartmentById method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: GetDepartmentById method execution started");

            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.deptid, deptid);
                var department = await con.QueryFirstOrDefaultAsync<Department>(StoredProcedures.GetDepartmentById, p, commandType: CommandType.StoredProcedure);
                Log.Information($"DepartmentRepository: GetDepartmentById method execution ended with DepartmentId:{deptid}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"DepartmentRepository: GetDepartmentById method ended with DepartmentId:{deptid}");
                return department;
            }

        }

        public async Task<List<Department>> GetDepartments()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: GetDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: GetDepartment method execution started");

            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                var departments = await con.QueryAsync<Department>(StoredProcedures.GetDepartment, commandType: CommandType.StoredProcedure);
                Log.Information("DepartmentRepository: GetDepartmentById method execution ended");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: GetDepartment method execution ended");
                return departments.ToList();
            }

        }

        public async Task<int> UpdateDepartment(Department department)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"DepartmentRepository: UpdateDepartment method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: UpdateGetDepartment method execution started");

            using (IDbConnection con = _connectionFactory.Northwind_DbConnectionString())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.deptid, department.deptid);
                p.Add(StoredProcedureParameters.deptname, department.deptname);
                p.Add(StoredProcedureParameters.deptlocation, department.deptlocation);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.UpdateDepartment, p, commandType: CommandType.StoredProcedure);
                Log.Information("DepartmentRepository: UpdateDepartment method execution ended");
                await _loggingFactory.AddLoggingMessages(userName, "information", "DepartmentRepository: UpdateDepartment method execution ended");

                return rowsAffected;
            }

        }
    }
}
