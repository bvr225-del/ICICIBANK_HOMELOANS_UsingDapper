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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserSignInResponse> UserResgistration(UsersDTO usersObj)
        {
            Users users = new Users();
            users.Id = usersObj.Id;
            users.UserName = usersObj.UserName;
            users.Password = usersObj.Password;
            users.EmailId = usersObj.EmailId;
            users.Address = usersObj.Address;
            users.PhoneNumber = usersObj.PhoneNumber;
            users.IsActive = usersObj.IsActive;
            var result = await _userRepository.UserResgistration(users);
            return result;

        }

        public async Task<UserSignInResponse> UserRolesMapping(UserRoleDTO userRoleDTOObj)
        {
            UserRole userRole = new UserRole();
            userRole.UserId = userRoleDTOObj.UserId;
            userRole.RoleId = userRoleDTOObj.RoleId;
            return await _userRepository.UserRolesMapping(userRole);

        }
    }
}
