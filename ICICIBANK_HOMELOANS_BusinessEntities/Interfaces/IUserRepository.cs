using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICICIBANK_HOMELOANS_BusinessEntities.Interfaces
{
    public interface IUserRepository
    {
        Task<UserSignInResponse> UserResgistration(Users usersObj);
        Task<UserSignInResponse> UserRolesMapping(UserRole userRoleObj);

    }
}
