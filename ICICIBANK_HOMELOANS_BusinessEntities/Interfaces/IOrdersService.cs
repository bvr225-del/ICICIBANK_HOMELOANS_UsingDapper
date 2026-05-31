using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IOrdersService
    {
        public Task<List<OrdersDto>> GetOrders();
        public Task<OrdersDto> GetOrderById(int orderid);
        public Task<int> AddOrder(OrdersDto orderDto);
        public Task<int> UpdateOrder(OrdersDto orderDto);
        public Task<bool> DeleteOrder(int orderid);
    }
}
