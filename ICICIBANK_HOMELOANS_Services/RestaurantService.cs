using AutoMapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantService(IRestaurantRepository restaurantRepository, IMapper mapper, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _restaurantRepository = restaurantRepository;
            this._mapper = mapper;
            this._loggingFactory = loggingFactory;
            this._httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddRestaurant(RestaurantDto restaurantDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service:AddRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:AddRestaurant API method execution started");

            Restaurant res =new Restaurant();
            _mapper.Map(restaurantDto, res);
            Log.Information("Restaurant Service:AddRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:AddRestaurant API method execution ended successfully");

            return await _restaurantRepository.AddRestaurant(res);
        }

        public async Task<bool> DeleteRestaurant(int restaurantId)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service:DeleteRestaurant API method execution startedand Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:DeleteRestaurant API method execution started");

            var result = await _restaurantRepository.DeleteRestaurant(restaurantId);
            Log.Information("Restaurant Service:DeleteRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:DeleteRestaurant API method execution ended successfully");

            return result;
        }

        public async Task<RestaurantDto> GetRestaurantById(int restaurantId)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service: GetRestaurantById API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: GetRestaurantById API method execution started");

            var res = await _restaurantRepository.GetRestaurantById(restaurantId);
            Log.Information("Restaurant Service: GetRestaurantById API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: GetRestaurantById API method execution ended successfully");

            return _mapper.Map<RestaurantDto>(res);
        }

        public async Task<List<RestaurantDto>> GetRestaurants()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service:GetallRestaurants API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:GetallRestaurants API method execution started");

            var res = await _restaurantRepository.GetRestaurants();
            Log.Information("Restaurant Service:GetallRestaurants API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service:GetallRestaurants API method execution ended successfully");

            return _mapper.Map<List<RestaurantDto>>(res);
        }

        public async Task<int> UpdateRestaurant(RestaurantDto restaurantDto)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Service: UpdateRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: UpdateRestaurant API method execution started");

            Restaurant res = new Restaurant();
            _mapper.Map(restaurantDto, res);
            var result= await _restaurantRepository.UpdateRestaurant(res);
            Log.Information("Restaurant Service: UpdateRestaurant API method execution ended successfully");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Service: UpdateRestaurant API method execution ended successfully");


            return result;

        }
    }
}
