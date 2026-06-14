using Dapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILoggingFactory _loggingFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RestaurantRepository(IConnectionFactory connectionFactory, ILoggingFactory loggingFactory, IHttpContextAccessor httpContextAccessor)
        {
            _connectionFactory = connectionFactory;
            _loggingFactory = loggingFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddRestaurant(Restaurant restaurant)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:AddRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:AddRestaurant API method execution started");

            using (IDbConnection con=_connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantname, restaurant.RestaurantName);
                parameters.Add(StoredProcedureParameters.restaurantlocation, restaurant.RestaurantLocation);
                parameters.Add(StoredProcedureParameters.restaurantInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteAsync(StoredProcedures.AddRestaurant, parameters, commandType: CommandType.StoredProcedure);
                int insertedId = parameters.Get<int>(StoredProcedureParameters.restaurantInsertedId);
                Log.Information($"Restaurant Repository:AddRestaurant API method execution ended with RestaurantInsertValue:insertedid ");
                await _loggingFactory.AddLoggingMessages(userName, "information", $"Restaurant Repository:AddRestaurant API method execution ended with RestaurantInsertValue:insertedid ");

                return insertedId;
            }
            
        }

        public async Task<bool> DeleteRestaurant(int restaurantid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:DeleteRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:DeleteRestaurant API method execution started");

            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantid, restaurantid);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.DeleteRestaurant, parameters, commandType: CommandType.StoredProcedure);
                Log.Information("Restaurant Repository:DeleteRestaurant API method execution ended succefully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:DeleteRestaurant API method execution ended successfully");

                return rowsAffected > 0;
            }

        }

        public async Task<Restaurant> GetRestaurantById(int restaurantid)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:GetRestaurantById API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetRestaurantById API method execution started");

            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantid, restaurantid);
                var restaurant = await con.QueryFirstOrDefaultAsync<Restaurant>(StoredProcedures.GetRestaurantById, parameters, commandType: CommandType.StoredProcedure);
                Log.Information($"Restaurant Repository:GetRestaurantById API method execution ended with Restaurant id:{restaurantid}");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetRestaurantById API method execution ended with Restaurant Id:{restaurantid}");

                return restaurant;
            }

        }

        public async Task<List<Restaurant>> GetRestaurants()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository:GetallRestaurants API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetallRestaurants API method execution started");

            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                var restaurants = await con.QueryAsync<Restaurant>(StoredProcedures.GetRestaurants, commandType: CommandType.StoredProcedure);
                Log.Information("Restaurant Repository:GetallRestaurants API method execution ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository:GetallRestaurants API method execution ended successfully");

                return restaurants.ToList();
            }

        }

        public async Task<int> UpdateRestaurant(Restaurant restaurant)
        {
            var userName = _httpContextAccessor.HttpContext?.User?.FindFirst("UserName")?.Value ?? "Unknown";

            Log.Information($"Restaurant Repository: UpdateRestaurant API method execution started and Current Loggedin username:{userName}");
            await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository: UpdateRestaurant API method execution started");

            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantid, restaurant.Id);
                parameters.Add(StoredProcedureParameters.restaurantname, restaurant.RestaurantName);
                parameters.Add(StoredProcedureParameters.restaurantlocation, restaurant.RestaurantLocation);
                //parameters.Add(StoredProcedureParameters.creationDate, restaurant.CreationDate);
                var rowsAffected = await con.ExecuteAsync(StoredProcedures.UpdateRestaurant, parameters, commandType: CommandType.StoredProcedure);
                Log.Information($"Restaurant Repository: UpdateRestaurant API method execution Ended successfully");
                await _loggingFactory.AddLoggingMessages(userName, "information", "Restaurant Repository: UpdateRestaurant API method execution ended successfully");

                return rowsAffected;
            }

        }
    }
}
