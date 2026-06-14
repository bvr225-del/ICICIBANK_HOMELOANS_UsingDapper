using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using ICICIBANK_HOMELOANS_Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(IOrdersService ordersService, ILoggingFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _ordersService = ordersService;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(OrdersDto orderDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"OrdersController:AddOrder API method execution started and Current Loggedin username:{userName}");
            Log.Information($"OrdersController:input parameter OrderName:{orderDto.ordername}");
            Log.Information($"OrdersController:input parameter OrderLocation:{orderDto.orderlocation}");
            #endregion

            #region Database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:AddOrder API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderName:{orderDto.ordername}");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderLocation:{orderDto.orderlocation}");
            #endregion 

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
            }
            else
            {
                int insertedId = await _ordersService.AddOrder(orderDto);
                Log.Information("OrdersController:AddOrder API method execution ended");
                await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:AddOrder API method execution ended");

                return StatusCode(StatusCodes.Status201Created, "created successfully");
            }
        }
        [HttpDelete]
        [Route("DeleteOrder/{orderid}")]
        public async Task<IActionResult> DeleteOrder(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"OrdersController:DeleteOrder API method execution started and Current Loggedin username:{userName}");
            Log.Information($"OrdersController:input parameter OrderId:{orderid}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:DeleteOrder API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderId:{orderid}");
            #endregion


            bool result = await _ordersService.DeleteOrder(orderid);
            if (result)
            {
                Log.Information("OrdersController:DeleteOrder API method execution ended");
                await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:DeleteOrder API method execution ended");

                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "order not found");
            }
        }
        [HttpGet]
        [Route("GetOrderById/{orderid}")]
        public async Task<IActionResult> GetOrderById(int orderid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"OrdersController:GetOrderById API method execution started and Current Loggedin username:{userName}");
            Log.Information($"OrdersController:input parameter OrderId:{orderid}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:GetOrderById API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderId:{orderid}");
            #endregion

            var order = await _ordersService.GetOrderById(orderid);
            if (order != null)
            {
                Log.Information("OrdersController:GetOrderById API method execution ended");
                await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:GetOrderById API method execution ended");
                return StatusCode(StatusCodes.Status200OK, order);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "order not found");
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"OrdersController:GetOrders API method execution started and Current Loggedin username:{userName}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:GetOrders API method execution started");
            #endregion 

            var orders = await _ordersService.GetOrders();
            if (orders == null || orders.Count == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "no orders found");
            }
            else
            {
                Log.Information("OrdersController:GetOrders API method execution started");
                await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:GetOrders API method execution started");

                return StatusCode(StatusCodes.Status200OK, orders);
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrdersDto orderDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"OrdersController:UpdateOrder API method execution started and Current Loggedin username:{userName}");
            Log.Information($"OrdersController:input parameter OrderId:{orderDto.orderid}");
            Log.Information($"OrdersController:input parameter OrderName:{orderDto.ordername}");
            Log.Information($"OrdersController:input parameter OrderLocation:{orderDto.orderlocation}");
            #endregion

            #region Database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:UpdateOrder API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderId:{orderDto.orderid}");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderName:{orderDto.ordername}");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"OrdersController:input parameter OrderLocation:{orderDto.orderlocation}");
            #endregion 

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
            }
            else
            {
                int rowsAffected = await _ordersService.UpdateOrder(orderDto);
                if (rowsAffected > 0)
                {
                    Log.Information("OrdersController:UpdateOrder API method execution ended");
                    await _loggerFactory.AddLoggingMessages(userName, "information", "OrdersController:UpdateOrder API method execution ended");

                    return StatusCode(StatusCodes.Status200OK, "updated successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "order not found");
                }
            }
        }
    }
}