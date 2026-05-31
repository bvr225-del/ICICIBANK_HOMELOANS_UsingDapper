using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RestaurantRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddRestaurant(Restaurant restaurant)
        {
            using(IDbConnection con=_connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantname, restaurant.RestaurantName);
                parameters.Add(StoredProcedureParameters.restaurantlocation, restaurant.RestaurantLocation);
                parameters.Add(StoredProcedureParameters.restaurantInsertedId, dbType: DbType.Int32, direction: ParameterDirection.Output);
                await con.ExecuteAsync(StoredProcedures.AddRestaurant, parameters, commandType: CommandType.StoredProcedure);
                int insertedId = parameters.Get<int>(StoredProcedureParameters.restaurantInsertedId);
                return insertedId;
            }
            
        }

        public async Task<bool> DeleteRestaurant(int restaurantid)
        {
            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantid, restaurantid);
                int rowsAffected = await con.ExecuteAsync(StoredProcedures.DeleteRestaurant, parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected > 0;
            }

        }

        public async Task<Restaurant> GetRestaurantById(int restaurantid)
        {
            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantid, restaurantid);
                var restaurant = await con.QueryFirstOrDefaultAsync<Restaurant>(StoredProcedures.GetRestaurantById, parameters, commandType: CommandType.StoredProcedure);
                return restaurant;
            }

        }

        public async Task<List<Restaurant>> GetRestaurants()
        {
            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                var restaurants = await con.QueryAsync<Restaurant>(StoredProcedures.GetRestaurants, commandType: CommandType.StoredProcedure);
                return restaurants.ToList();
            }

        }

        public async Task<int> UpdateRestaurant(Restaurant restaurant)
        {
            using (IDbConnection con = _connectionFactory.Restaurant_DbConnectionString())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add(StoredProcedureParameters.restaurantid, restaurant.Id);
                parameters.Add(StoredProcedureParameters.restaurantname, restaurant.RestaurantName);
                parameters.Add(StoredProcedureParameters.restaurantlocation, restaurant.RestaurantLocation);
                //parameters.Add(StoredProcedureParameters.creationDate, restaurant.CreationDate);
                var rowsAffected = await con.ExecuteAsync(StoredProcedures.UpdateRestaurant, parameters, commandType: CommandType.StoredProcedure);
                return rowsAffected;
            }

        }
    }
}
