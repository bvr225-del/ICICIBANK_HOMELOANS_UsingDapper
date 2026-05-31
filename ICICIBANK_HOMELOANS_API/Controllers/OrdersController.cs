using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        [HttpPost]
        [Route("AddOrder")]
        public async Task<IActionResult> AddOrder(OrdersDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int insertedId = await _ordersService.AddOrder(orderDto);
                    return StatusCode(StatusCodes.Status201Created, "created successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpDelete]
        [Route("DeleteOrder/{orderid}")]
        public async Task<IActionResult> DeleteOrder(int orderid)
        {
            try
            {
                bool result = await _ordersService.DeleteOrder(orderid);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "order not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpGet]
        [Route("GetOrderById/{orderid}")]
        public async Task<IActionResult> GetOrderById(int orderid)
        {
            try
            {
                var order = await _ordersService.GetOrderById(orderid);
                if (order != null)
                {
                    return StatusCode(StatusCodes.Status200OK, order);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "order not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpGet]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _ordersService.GetOrders();
                if (orders == null || orders.Count == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "no orders found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, orders);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpPut]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(OrdersDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int rowsAffected = await _ordersService.UpdateOrder(orderDto);
                    if (rowsAffected > 0)
                    {
                        return StatusCode(StatusCodes.Status200OK, "updated successfully");
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "order not found");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        }
}