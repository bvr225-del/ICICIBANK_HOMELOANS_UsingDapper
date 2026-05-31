using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Utils
{
    public static class StoredProcedures
    {
        #region Employee stored procedures
        public static string GetEmployees= "Usp_GetEmployee";
        public static string DeleteEmployee = "Usp_DeleteEmployee";
        public static string GetEmployeeById = "Usp_GetEmployeeById";
        public static string AddEmployee = "Usp_AddEmployee";
        public static string UpdateEmployee = "Usp_UpdateEmployee";
        #endregion

        #region Orders stored procedures
        public static string GetOrders = "Usp_GetOrders";
        public static string GetOrderById = "Usp_GetOrderById";
        public static string AddOrder = "Usp_AddOrder";
        public static string UpdateOrder = "Usp_UpdateOrder";
        public static string DeleteOrder = "Usp_DeleteOrder";
        #endregion
    }
}
