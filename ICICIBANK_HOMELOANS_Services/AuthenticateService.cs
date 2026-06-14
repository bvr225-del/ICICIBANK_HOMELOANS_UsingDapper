using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _authenticateRepository;
        public AuthenticateService(IAuthenticateRepository authenticateRepository)
        {
            _authenticateRepository = authenticateRepository;
        }
        public async Task<UserRolesInformationResponse> GetUserRolesInformation(LoginDTO loginDTOObj)
        {
            var res = await _authenticateRepository.GetUserRolesInformation(loginDTOObj);
            return res;

        }

        public async Task<UserSignInResponse> UserSignIn(LoginDTO loginDTOObj)
        {
            var res = await _authenticateRepository.UserSignIn(loginDTOObj);
            return res;
        }
    }
}
