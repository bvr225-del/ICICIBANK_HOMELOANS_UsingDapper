using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IOrderRepository
    {
        public Task<List<Orders>> GetOrders();
        public Task<Orders> GetOrderById(int orderid);
         public Task<int> AddOrder(Orders order);
         public Task<int> UpdateOrder(Orders order);
         public Task<bool> DeleteOrder(int orderid);
    }
}
