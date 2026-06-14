using ICICIBANK_HOMELOANS_BusinessEntities.Dtos;
using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICICIBANK_HOMELOANS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #region User Registration
        [HttpPost]
        [Route("UserRegistartion")]
        public async Task<IActionResult> UserRegistartion([FromBody] UsersDTO usersDTO)
        {//Singup/Register both are same ,use this api fro user registration or user signup.
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var res = await _userService.UserResgistration(usersDTO);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region User RolesMapping
        [HttpPost]
        [Route("UserRolesMapping")]
        public async Task<IActionResult> UserRolesMapping([FromBody] UserRoleDTO userRoleDTOObj)
        {

            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
            else
            {
                var res = await _userService.UserRolesMapping(userRoleDTOObj);
                return Ok(res);
            }
        }
        #endregion
    }
}
