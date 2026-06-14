using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ILoggingFactory _loggerFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantController(IRestaurantService restaurantService, ILoggingFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _restaurantService = restaurantService;
            _loggerFactory = loggerFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpPost]
        [Route("AddRestaurant")]
        public async Task<IActionResult> AddRestaurant(RestaurantDto restaurantDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"RestaurantController:AddRestaurant API method execution started and Current Loggedin username:{userName}");
            Log.Information($"RestaurantController:called input parameter Restaurant Name:{restaurantDto.RestaurantName}");
            Log.Information($"RestaurantController:called input parameter Restaurant Location:{restaurantDto.RestaurantLocation}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:AddRestaurant API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant Name:{restaurantDto.RestaurantName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant Location:{restaurantDto.RestaurantLocation}");
            #endregion

            if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int insertedId = await _restaurantService.AddRestaurant(restaurantDto);
                Log.Information("RestaurantController:AddRestaurant API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:AddRestaurant API method execution ended successfully");

                return StatusCode(StatusCodes.Status201Created, "created successfully");
                }

        }
        [HttpDelete]
        [Route("DeleteRestaurant/{Id}")]
        public async Task<IActionResult> DeleteRestaurant(int Id)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"RestaurantController:DeleteRestaurantById API method execution started and Current Loggedin username:{userName}");
            Log.Information($"RestaurantController:called input parameter Restaurant ID:{Id}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:DeleteRestaurantById API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant ID:{Id}");
            #endregion

            bool result = await _restaurantService.DeleteRestaurant(Id);
                if (result)
                {
                Log.Information("RestaurantController:DeleteRestaurantById API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages("venkat", "information", "RestaurantController:DeleteRestaurantById API method execution ended successfully");
                return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurant not found");
                }
        }
        [HttpGet]
        [Route("GetRestaurantById/{restaurantid}")]
        public async Task<IActionResult> GetRestaurantById(int restaurantid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"RestaurantController:GetRestaurantById API method execution started and Current Loggedin username:{userName}");
            Log.Information($"RestaurantController:called input parameter Restaurant ID:{restaurantid}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:GetRestaurantById API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant ID:{restaurantid}");
            #endregion


            var restaurant = await _restaurantService.GetRestaurantById(restaurantid);
                if (restaurant != null)
                {
                Log.Information("RestaurantController:GetRestaurantById API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:GetRestaurantById API method execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, restaurant);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurant not found");
                }
        }
        [HttpGet]
        [Route("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"RestaurantController:GetRestaurants API method execution started and Current Loggedin username:{userName}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:GetRestaurants API method execution started");
            #endregion


            var restaurants = await _restaurantService.GetRestaurants();
                if (restaurants != null && restaurants.Count > 0)
                {
                Log.Information("RestaurantController:GetRestaurants API method execution ended successfully");
                await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:GetRestaurants API method execution ended successfully");

                return StatusCode(StatusCodes.Status200OK, restaurants);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "no restaurant data found");
                }
        }
        [HttpPut]
        [Route("UpdateRestaurant")]
        public async Task<IActionResult> UpdateRestaurant(RestaurantDto restaurantDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            #region serilog
            Log.Information($"RestaurantController:UpdateRestaurant API method execution started and Current Loggedin username:{userName}");
            Log.Information($"RestaurantController:called input parameter Restaurant ID:{restaurantDto.Id}");
            Log.Information($"RestaurantController:called input parameter Restaurant Name:{restaurantDto.RestaurantName}");
            Log.Information($"RestaurantController:called input parameter Restaurant Location:{restaurantDto.RestaurantLocation}");
            #endregion
            #region database log
            await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:UpdateRestaurant API method execution started");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant ID:{restaurantDto.Id}");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant Name:{restaurantDto.RestaurantName}");
            await _loggerFactory.AddLoggingMessages(userName, "information", $"RestaurantController:called input parameter Restaurant Location:{restaurantDto.RestaurantLocation}");
            #endregion

            if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    var rowsAffected = await _restaurantService.UpdateRestaurant(restaurantDto);
                    if (rowsAffected == 0)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "restaurant not found");
                    }
                    else
                    {
                    Log.Information("RestaurantController:UpdateRestaurant API method execution ended successfully");
                    await _loggerFactory.AddLoggingMessages(userName, "information", "RestaurantController:UpdateRestaurant API method execution ended successfully");

                    return StatusCode(StatusCodes.Status200OK, "updated successfully");
                    }
                }
        }
    }
}