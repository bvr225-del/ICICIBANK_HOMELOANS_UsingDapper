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
    public class OrdersRepository : IOrderRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public OrdersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddOrder(Orders order)
        {
            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.ordername, order.ordername);
                p.Add(StoredProcedureParameters.orderlocation, order.orderlocation);
                p.Add(StoredProcedureParameters.orderInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteAsync(StoredProcedures.AddOrder, p, commandType: CommandType.StoredProcedure);
                int insertedId = p.Get<int>(StoredProcedureParameters.orderInsertedId);
                return insertedId;
            }

        }

        public async Task<bool> DeleteOrder(int orderid)
        {
            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.orderid, orderid);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.DeleteOrder, p, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }


        }

        public async Task<Orders> GetOrderById(int orderid)
        {
            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.orderid, orderid);
                var order = await con.QueryFirstOrDefaultAsync<Orders>(StoredProcedures.GetOrderById, p, commandType: CommandType.StoredProcedure);
                return order;
            }

        }

        public async Task<List<Orders>> GetOrders()
        {
            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                var orders = await con.QueryAsync<Orders>(StoredProcedures.GetOrders, commandType: CommandType.StoredProcedure);
                return orders.ToList();
            }

        }

        public async Task<int> UpdateOrder(Orders order)
        {
            using (IDbConnection con = _connectionFactory.midlandsqlconnectionstring())
            {
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.orderid, order.orderid);
                p.Add(StoredProcedureParameters.ordername, order.ordername);
                p.Add(StoredProcedureParameters.orderlocation, order.orderlocation);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.UpdateOrder, p, commandType: CommandType.StoredProcedure);
                return rowsAffected;

            }
        }
    }
}