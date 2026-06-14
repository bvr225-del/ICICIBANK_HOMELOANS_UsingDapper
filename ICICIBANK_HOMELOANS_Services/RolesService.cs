using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }
        public async Task<UserSignInResponse> RolesCreation(RolesDTO rolesObj)
        {
            Roles roles = new Roles();
            roles.RoleName = rolesObj.RoleName;
            roles.IsActive = rolesObj.IsActive;
            var result = await _rolesRepository.RolesCreation(roles);
            return result;

        }
    }
}
