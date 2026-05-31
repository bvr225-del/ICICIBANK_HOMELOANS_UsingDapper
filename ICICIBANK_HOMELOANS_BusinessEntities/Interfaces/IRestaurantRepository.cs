using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetRestaurants();
        Task<Restaurant> GetRestaurantById(int restaurantid);
        Task<int> AddRestaurant(Restaurant restaurant);
        Task<int> UpdateRestaurant(Restaurant restaurant);
        Task<bool> DeleteRestaurant(int restaurantid);
    }
}
