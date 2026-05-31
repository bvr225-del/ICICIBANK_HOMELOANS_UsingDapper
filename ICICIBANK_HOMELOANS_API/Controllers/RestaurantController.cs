using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpPost]
        [Route("AddRestaurant")]
        public async Task<IActionResult> AddRestaurant(RestaurantDto restaurantDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid data");
                }
                else
                {
                    int insertedId = await _restaurantService.AddRestaurant(restaurantDto);
                    return StatusCode(StatusCodes.Status201Created, "created successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }

        }
        [HttpDelete]
        [Route("DeleteRestaurant/{Id}")]
        public async Task<IActionResult> DeleteRestaurant(int Id)
        {
            try
            {

                bool result = await _restaurantService.DeleteRestaurant(Id);
                if (result)
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurant not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpGet]
        [Route("GetRestaurantById/{restaurantid}")]
        public async Task<IActionResult> GetRestaurantById(int restaurantid)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantById(restaurantid);
                if (restaurant != null)
                {
                    return StatusCode(StatusCodes.Status200OK, restaurant);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "restaurant not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpGet]
        [Route("GetRestaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var restaurants = await _restaurantService.GetRestaurants();
                if (restaurants != null && restaurants.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, restaurants);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "no restaurant data found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "internal error occurred");
            }
        }
        [HttpPut]
        [Route("UpdateRestaurant")]
        public async Task<IActionResult> UpdateRestaurant(RestaurantDto restaurantDto)
        {
            try
            {
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
                        return StatusCode(StatusCodes.Status200OK, "updated successfully");
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