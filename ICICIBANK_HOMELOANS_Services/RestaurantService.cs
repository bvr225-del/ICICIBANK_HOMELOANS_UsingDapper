using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;

namespace ICICIBANK_HOMELOANS_Services
{
    public class RestaurantService : IRestaurantService
    {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IMapper _mapper;
        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _restaurantRepository = restaurantRepository;
            this._mapper = mapper;
        }
        public async Task<int> AddRestaurant(RestaurantDto restaurantDto)
        {
            Restaurant res=new Restaurant();
            _mapper.Map(restaurantDto, res);
            return await _restaurantRepository.AddRestaurant(res);
        }

        public async Task<bool> DeleteRestaurant(int restaurantId)
        {
            var result = await _restaurantRepository.DeleteRestaurant(restaurantId);
            return result;
        }

        public async Task<RestaurantDto> GetRestaurantById(int restaurantId)
        {
           var res= await _restaurantRepository.GetRestaurantById(restaurantId);
            return _mapper.Map<RestaurantDto>(res);
        }

        public async Task<List<RestaurantDto>> GetRestaurants()
        {
            var res = await _restaurantRepository.GetRestaurants();
            return _mapper.Map<List<RestaurantDto>>(res);
        }

        public async Task<int> UpdateRestaurant(RestaurantDto restaurantDto)
        {
            Restaurant res = new Restaurant();
            _mapper.Map(restaurantDto, res);
            var result= await _restaurantRepository.UpdateRestaurant(res);
            return result;

        }
    }
}
