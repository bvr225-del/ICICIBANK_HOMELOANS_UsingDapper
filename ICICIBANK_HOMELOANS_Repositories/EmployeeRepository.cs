using Dapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using ICICIBANK_HOMELOANS_DbConnectivity.Data;
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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmployeeRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddEmployee(Employee employee)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: AddEmployees method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: AddEmployee  method execution starts");

            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empname, employee.empname);
                p.Add(StoredProcedureParameters.empsalary, employee.empsalary);
                p.Add(StoredProcedureParameters.employeeInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredProcedures.AddEmployee, p, commandType: CommandType.StoredProcedure);
                int insertedId = p.Get<int>(StoredProcedureParameters.employeeInsertedId);
                Log.Information("EmployeeRepository: AddEmployee  method execution completed successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: AddEmployee  method execution completed successfully");

                Log.Information($"EmployeeRepository:AddEmployee Method executed successfully with EmployeeInsertedId:{insertedId}");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeRepository:AddEmployee Method executed successfully with EmployeeInsertedId:{insertedId}");

                return insertedId;
            }

        }

        public async Task<string> DeleteEmployee(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: DeleteEmployeeById method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: DeleteEmployee  method execution starts");

            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empid, empid);
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployeeById, p, commandType: CommandType.StoredProcedure);
                Employee employee = result.FirstOrDefault();
                if (employee == null)
                {
                    return $"Employee id {empid} empid does not exist in database";
                }
                else
                {
                    var deletedData = $"Deleted Employee:ID={employee.empid},Name={employee.empname},Salary={employee.empsalary}";
                    var result1 = await con.ExecuteScalarAsync<string>(StoredProcedures.DeleteEmployee, p, commandType: CommandType.StoredProcedure);
                    Log.Information("EmployeeRepository: DeleteEmployee  method execution completed successfully");
                    await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: DeleteEmployee  method execution completed successfully");
                    Log.Information($"EmployeeRepository: Employee with id {empid} deleted successfully");
                    await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeRepository: Employee with id {empid} deleted successfully");

                    return deletedData;
                }
            }
        }

        public async Task<Employee> GetEmployeeById(int empid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: GetEmployeeById method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: GetEmployeeById  method execution starts");

            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empid, empid);
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployeeById, p, commandType: CommandType.StoredProcedure);
                Employee employee = result.FirstOrDefault();
                Log.Information("EmployeeRepository: GetEmployeeById  method execution completed successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: GetEmployeeById  method execution completed successfully");

                return employee;
            }
        }
        public async Task<List<Employee>> GetEmployees()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"EmployeeRepository: GetEmployees method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: GetAllEmployees  method execution starts");

            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployees, commandType: CommandType.StoredProcedure);
                Log.Information("EmployeeRepository: GetAllEmployees  method execution completed successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: GetAllEmployees  method execution completed successfully");
                return result.ToList();
            }

        }

        public async Task<string> UpdateEmployee(Employee employee)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";
            Log.Information($"EmployeeRepository:UpdateEmployee  method Excution Starts and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: UpdateEmployee  method execution starts");

            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empid, employee.empid);
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployeeById, p, commandType: CommandType.StoredProcedure);
                Employee employee1 = result.FirstOrDefault();
                if (employee1 == null)
                {
                    Log.Information($"EmployeeRepository: Employee with id {employee.empid} not found for update");
                    await _loggingFactory.AddLoggingMessages(userName, "information", $"EmployeeRepository: Employee with id {employee.empid} not found for update");

                    return $"Employee id {employee.empid} empid does not exist in database";
                    
                }
                else
                {
                    var updateEmployee = $"Updated Employee:ID={employee.empid},Name={employee.empname},Salary={employee.empsalary}";
                    p.Add(StoredProcedureParameters.empid, employee.empid);
                    p.Add(StoredProcedureParameters.empname, employee.empname);
                    p.Add(StoredProcedureParameters.empsalary, employee.empsalary);
                     await con.ExecuteScalarAsync<string>(StoredProcedures.UpdateEmployee, p, commandType: CommandType.StoredProcedure);
                    Log.Information("EmployeeRepository: UpdateEmployee  method execution completed successfully");
                    await _loggingFactory.AddLoggingMessages(userName, "information", "EmployeeRepository: UpdateEmployee  method execution completed successfully");
                    return updateEmployee;

                }
            }
        }
    }
}