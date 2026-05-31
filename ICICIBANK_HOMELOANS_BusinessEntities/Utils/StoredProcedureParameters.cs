using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Utils
{
    public static class StoredProcedureParameters
    {
        #region Employee stored procedure parameters
        public static string empid = "@empid";
        public static string empname = "@empname";
        public static string empsalary = "@empsalary";
        public static string employeeInsertedId = "@insertvalue";
        #endregion

        #region Orders stored procedure parameters
        public static string orderid = "@orderid";
        public static string ordername = "@ordername";
        public static string orderlocation = "@orderlocation";
        public static string orderInsertedId = "@insertvalue";
        #endregion

        #region Department stored procedure parameters
        public static string deptid = "@deptid";
        public static string deptname = "@deptname";
        public static string deptlocation = "@deptlocation";
        public static string departmentInsertedId = "@insertvalue";
        #endregion

        #region Restaurant stored procedure parameters
        public static string restaurantid = "@Id";
        public static string restaurantname = "@RestaurantName";
        public static string restaurantlocation = "@RestaurantLocation";
        public static string creationDate = "@CreationDate";
        public static string restaurantInsertedId = "@insertvalue";
        #endregion
    }
}
