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
    public class OrdersRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersRepository(IConnectionFactory connectionFactory, ILoggingFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddOrder(Orders order)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:AddOrder API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:AddOrder API method execution started");

            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.ordername, order.ordername);
                p.Add(StoredProcedureParameters.orderlocation, order.orderlocation);
                p.Add(StoredProcedureParameters.orderInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteAsync(StoredProcedures.AddOrder, p, commandType: CommandType.StoredProcedure);
                int insertedId = p.Get<int>(StoredProcedureParameters.orderInsertedId);
                Log.Information($"Orders Repository:AddOrder API method execution ended with OrdersInsertValue:insertvalue");
                await _loggerFactory.AddLoggingMessages(userName, "information", $"Orders Repository:AddOrder API method execution ended with OrdersInsertValue:insertvalue");

                return insertedId;
            }

        }

        public async Task<bool> DeleteOrder(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:DeleteOrder API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:DeleteOrder API method execution started");

            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.orderid, orderid);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.DeleteOrder, p, commandType: CommandType.StoredProcedure);
                Log.Information("Orders Repository:DeleteOrder API method execution ended");
                await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:DeleteOrder API method execution ended");

                return rowsAffected > 0;
            }


        }

        public async Task<Orders> GetOrderById(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:GetOrderById API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrderById API method execution started");

            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.orderid, orderid);
                var order = await con.QueryFirstOrDefaultAsync<Orders>(StoredProcedures.GetOrderById, p, commandType: CommandType.StoredProcedure);
                Log.Information("Orders Repository:GetOrderById API method execution ended");
                await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrderById API method execution completed");

                return order;
            }

        }

        public async Task<List<Orders>> GetOrders()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:GetOrders API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrders API method execution started");

            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                var orders = await con.QueryAsync<Orders>(StoredProcedures.GetOrders, commandType: CommandType.StoredProcedure);
                Log.Information("Orders Repository:GetOrders API method execution completed");
                await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:GetOrders API method execution completed");


                return orders.ToList();
            }

        }

        public async Task<int> UpdateOrder(Orders order)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Orders Repository:UpdateOrder API method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:UpdateOrder API method execution started");

            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.orderid, order.orderid);
                p.Add(StoredProcedureParameters.ordername, order.ordername);
                p.Add(StoredProcedureParameters.orderlocation, order.orderlocation);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.UpdateOrder, p, commandType: CommandType.StoredProcedure);
                Log.Information("Orders Repository:UpdateOrder API method execution completed");
                await _loggerFactory.AddLoggingMessages(userName, "information", "Orders Repository:UpdateOrder API method execution completed");

                return rowsAffected;

            }
        }
    }
}