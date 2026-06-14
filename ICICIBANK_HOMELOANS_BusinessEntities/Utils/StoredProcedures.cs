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

        #region Department StoredProcedure
        public static string GetDepartment= "Usp_GetDepartments";
        public static string GetDepartmentById = "Usp_GetDepartmentById";
        public static string AddDepartment = "Usp_AddDepartment";
        public static string UpdateDepartment = "Usp_UpdateDepartment";
        public static string DeleteDepartment = "Usp_DeleteDepartment";
        #endregion

        #region Restaurant stored procedures
            public static string GetRestaurants = "Usp_GetRestaurant";
            public static string GetRestaurantById = "Usp_GetRestaurantById";
            public static string AddRestaurant = "Usp_AddRestaurant";
            public static string UpdateRestaurant = "Usp_UpdateRestaurant";
            public static string DeleteRestaurant = "Usp_DeleteRestaurant";
        #endregion

        #region ProjectLevelLog Stored Procedures
        public static string AddLoggingMessages = "Usp_ProjectLevelLog";
        #endregion

        #region ProjectLevelErrorLog Stored Procedures
        public static string AddProjectLevelErrorLog = "Usp_AddProjectLevelErrorlog";
        #endregion

        #region TokenBasedAuthentication storedprocedures
        public static readonly string GetUserRolesInformation = "Usp_GetUserRolesInformation";

        public static readonly string SignIn = "Usp_LoginCheck";

        public static readonly string Usp_UserResgistration = "Usp_UserResgistration";

        public static readonly string Usp_RolesResgistration = "Usp_RolesResgistration";

        public static readonly string Usp_UserRolesMapping = "Usp_UserRolesMapping";

        #endregion

    }
}
