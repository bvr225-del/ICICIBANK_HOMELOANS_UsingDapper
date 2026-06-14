using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Repositories.MIddleWares
{
    public class AppException: Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        protected AppException(string message, Exception innerException) : base(message, innerException) { }
    }
}
