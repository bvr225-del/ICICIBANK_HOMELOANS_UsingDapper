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
    }
}
