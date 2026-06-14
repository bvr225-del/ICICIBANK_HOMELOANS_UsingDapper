using Dapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RolesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<UserSignInResponse> RolesCreation(Roles rolesObj)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                var p = new DynamicParameters();
                p.Add("@RoleName", rolesObj.RoleName);
                p.Add("@IsActive", rolesObj.IsActive);
                var result = await con.QuerySingleAsync<UserSignInResponse>(StoredProcedures.Usp_RolesResgistration, p, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
