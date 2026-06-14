using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface ILoggingFactory
    {
        Task<bool> AddLoggingMessages(string username, string LogLevel, string MessageTemplate);
        Task<bool> AddProjectLevelErrorLogAsync(string StatusCode, string ErrorMessage, string StackTraceError, string InnerExceptionError, string userName);
    }
}
