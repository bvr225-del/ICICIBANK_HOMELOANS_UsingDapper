using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IConnectionFactory
    {
        SqlConnection hotelmanagementsqlconnectionstring();
        SqlConnection midlandsqlconnectionstring();

        SqlConnection Northwind_DbConnectionString();
    }
}
