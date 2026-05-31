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
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersService(IOrderRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            this._mapper = mapper;
        }

        public async Task<int> AddOrder(OrdersDto orderDto)
        {
            Orders order=new Orders();
            _mapper.Map(orderDto, order);
            return await _ordersRepository.AddOrder(order);

        }

        public async Task<bool> DeleteOrder(int orderid)
        {
            var result = await _ordersRepository.DeleteOrder(orderid);
            return result;

        }

        public async Task<OrdersDto> GetOrderById(int orderid)
        {
         var  res =await _ordersRepository.GetOrderById(orderid);
            return _mapper.Map<OrdersDto>(res);


        }

        public async Task<List<OrdersDto>> GetOrders()
        {
            var res = await _ordersRepository.GetOrders();
            return _mapper.Map<List<OrdersDto>>(res);

        }

        public async Task<int> UpdateOrder(OrdersDto orderDto)
        {
            Orders order = new Orders();
            _mapper.Map(orderDto, order);
            return await _ordersRepository.UpdateOrder(order);

        }
    }
}
