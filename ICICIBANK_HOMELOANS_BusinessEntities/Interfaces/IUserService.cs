using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IUserService
    {
        Task<UserSignInResponse> UserResgistration(UsersDTO usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRoleDTO userRoleDTOObj);

    }
}
