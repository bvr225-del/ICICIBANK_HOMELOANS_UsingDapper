using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_DbConnectivity.Data
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection hotelmanagementsqlconnectionstring()
        {
            var connectionString=Convert.ToString(_configuration.GetSection(ConnectionStringNames.hotelmanagementsqlconnectionstring).Value);
            SqlConnection con= new SqlConnection(connectionString);
            return con;

        }

        public SqlConnection midlandsqlconnectionstring()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(ConnectionStringNames.midlandsqlconnectionstring).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public SqlConnection Northwind_DbConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(ConnectionStringNames.Northwind_DbConnectionString).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }

        public SqlConnection Restaurant_DbConnectionString()
        {
            var connectionString = Convert.ToString(_configuration.GetSection(ConnectionStringNames.Restaurant_DbConnectionString).Value);
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}
