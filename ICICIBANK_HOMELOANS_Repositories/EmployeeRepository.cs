using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public EmployeeRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddEmployee(Employee employee)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empname, employee.empname);
                p.Add(StoredProcedureParameters.empsalary, employee.empsalary);
                p.Add(StoredProcedureParameters.employeeInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteScalarAsync<int>(StoredProcedures.AddEmployee, p, commandType: CommandType.StoredProcedure);
                int insertedId = p.Get<int>(StoredProcedureParameters.employeeInsertedId);
                return insertedId;
            }

        }

        public async Task<string> DeleteEmployee(int empid)
        {
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
                    return deletedData;
                }
            }
        }

        public async Task<Employee> GetEmployeeById(int empid)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empid, empid);
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployeeById, p, commandType: CommandType.StoredProcedure);
                Employee employee = result.FirstOrDefault();
                return employee;
            }
        }
        public async Task<List<Employee>> GetEmployees()
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployees, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }



        }

        public async Task<string> UpdateEmployee(Employee employee)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.empid, employee.empid);
                var result = await con.QueryAsync<Employee>(StoredProcedures.GetEmployeeById, p, commandType: CommandType.StoredProcedure);
                Employee employee1 = result.FirstOrDefault();
                if (employee1 == null)
                {
                    return $"Employee id {employee.empid} empid does not exist in database";
                    
                }
                else
                {
                    var updateEmployee = $"Updated Employee:ID={employee.empid},Name={employee.empname},Salary={employee.empsalary}";
                    p.Add(StoredProcedureParameters.empid, employee.empid);
                    p.Add(StoredProcedureParameters.empname, employee.empname);
                    p.Add(StoredProcedureParameters.empsalary, employee.empsalary);
                     await con.ExecuteScalarAsync<string>(StoredProcedures.UpdateEmployee, p, commandType: CommandType.StoredProcedure);
                    return updateEmployee;

                }
            }
        }
    }
}