using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IRestaurantService
    {
        Task<List<RestaurantDto>> GetRestaurants();
        Task<RestaurantDto> GetRestaurantById(int restaurantId);
        Task<int> AddRestaurant(RestaurantDto restaurantDto);
        Task<int> UpdateRestaurant(RestaurantDto restaurantDto);
        Task<bool> DeleteRestaurant(int restaurantId);
    }
}
