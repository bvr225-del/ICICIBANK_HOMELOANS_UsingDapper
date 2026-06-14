using Dapper;
using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public AuthenticateRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                var p = new DynamicParameters();
                p.Add("@UserName", loginDTOObj.UserName);
                //var queryResult = await conn.QueryAsync<Hotel>(StoredProcedureStaticMessages.GetHotelDetails, CommandType.StoredProcedure);
                var result = await con.QueryAsync<UserRolesInformationResponse>(StoredProcedures.GetUserRolesInformation, p, commandType: CommandType.StoredProcedure);
                var status = result.FirstOrDefault();
                return status;
            }
        }
        public async Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj)
        {
            using (IDbConnection con = _connectionFactory.hotelmanagementsqlconnectionstring())
            {
                var encryptText = EncryptionLibrary.EncryptText(loginDTOObj.Password);
                //==========******For Testing Point of view you  can see the  decrypt text=======
                var decryptText = EncryptionLibrary.DecryptText(encryptText);
                //===========================================================================
                var p = new DynamicParameters();
                p.Add("@UserName", loginDTOObj.UserName);
                p.Add("@Password", encryptText);
                //var queryResult = await conn.QueryAsync<Hotel>(StoredProcedureStaticMessages.GetHotelDetails, CommandType.StoredProcedure);
                var result = await con.QueryAsync<UserSignInResponse>(StoredProcedures.SignIn, p, commandType: CommandType.StoredProcedure);
                var status = result.FirstOrDefault();
                return status;
            }
        }
    }
}
