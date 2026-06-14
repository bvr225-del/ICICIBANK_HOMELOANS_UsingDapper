using AutoMapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using ICICIBANK_HOMELOANS_Repositories;
using Microsoft.AspNetCore.Http;
using Serilog;
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
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersService(IOrderRepository ordersRepository, IMapper mapper, ILoggingFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _ordersRepository = ordersRepository;
            this._mapper = mapper;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> AddOrder(OrdersDto orderDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrdersService:AddOrder api method execution started and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:AddOrder api method execution started");

            Orders order =new Orders();
            _mapper.Map(orderDto, order);
            Log.Information("OrdersService:AddOrder api method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:AddOrder api method execution completed");

            return await _ordersRepository.AddOrder(order);

        }

        public async Task<bool> DeleteOrder(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: DeleteOrder  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:DeleteOrder api method execution started");


            var result = await _ordersRepository.DeleteOrder(orderid);
            Log.Information("OrderService: DeleteOrder  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService:DeleteOrder api method execution completed");

            return result;

        }

        public async Task<OrdersDto> GetOrderById(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: GetOrderById Order  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrderById api method execution started");

            var res =await _ordersRepository.GetOrderById(orderid);
            Log.Information("OrderService: GetOrderById Order  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrderById api method execution completed");

            return _mapper.Map<OrdersDto>(res);


        }

        public async Task<List<OrdersDto>> GetOrders()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: GetOrders Order  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrders api method execution started");

            var res = await _ordersRepository.GetOrders();
            Log.Information("OrderService: GetOrders Order  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: GetOrders api method execution completed");

            return _mapper.Map<List<OrdersDto>>(res);

        }

        public async Task<int> UpdateOrder(OrdersDto orderDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"OrderService: UpdateOrder Order  method execution starts and Current Loggedin username:{userName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: UpdateOrder api method execution started");

            Orders order = new Orders();
            _mapper.Map(orderDto, order);
            Log.Information("OrderService: UpdateOrder Order  method execution completed");
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersService: UpdateOrder api method execution completed");

            return await _ordersRepository.UpdateOrder(order);

        }
    }
}
