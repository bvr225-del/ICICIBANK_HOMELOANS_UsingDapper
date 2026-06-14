using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ICICIBANK_HOMELOANS_DbConnectivity.Data
{
    public class LoggingFactory : ILoggingFactory
    {
        #region connectionFactory
        private readonly IConnectionFactory _connectionFactory;
        public LoggingFactory(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        #endregion


        public async Task<bool> AddLoggingMessages(string username, string logLevel, string messageTemplate)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                //DynamicParameters used in dapper,to pass the values to storedprocedure parameters.
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.Username, username);
                p.Add(StoredProcedureParameters.LogLevel, logLevel);
                p.Add(StoredProcedureParameters.MessageTemplate, messageTemplate);
                await con.ExecuteScalarAsync(StoredProcedures.AddLoggingMessages, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }


        public async Task<bool> AddProjectLevelErrorLogAsync(string StatusCode, string ErrorMessage, string StackTraceError, string InnerExceptionError, string userName)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                //DynamicParameters used in dapper,to pass the values to storedprocedure parameters.
                DynamicParameters p = new DynamicParameters();
                p.Add(StoredProcedureParameters.StatusCode, StatusCode);
                p.Add(StoredProcedureParameters.ErrorMessage, ErrorMessage);
                p.Add(StoredProcedureParameters.StackTraceError, StackTraceError);
                p.Add(StoredProcedureParameters.InnerExceptionError, InnerExceptionError);
                p.Add(StoredProcedureParameters.UserName, userName);//Here pass the username to Storedprocedure.
                await con.ExecuteScalarAsync(StoredProcedures.AddProjectLevelErrorLog, p, commandType: CommandType.StoredProcedure);
                return true;
            }

        }
    }
}
