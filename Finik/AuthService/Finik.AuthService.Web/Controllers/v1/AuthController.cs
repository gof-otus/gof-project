using Asp.Versioning;
using Finik.AuthService.Contracts;
using Finik.AuthService.Core;
using Finik.AuthService.Web.Attributes;
using Finik.AuthService.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finik.AuthService.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    [AuthRole(Role.Administrator)]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IAuthManager _authManager;

        public AuthController(IUserManager userManager, IAuthManager authManager)
        {
            _userManager = userManager;
            _authManager = authManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var users = await _userManager.GetAllUsers();
            var user =  users.SingleOrDefault(u => u.Email.Equals(loginRequest.Email) && u.Password.Equals(loginRequest.Password));

            if (user != null) 
            {
                var token = _authManager.GenerateToken(user);
                return Ok(token);              
            }

            return Unauthorized();
        }

        [HttpGet("users/{id:guid?}")]
        public async Task<ActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _userManager.GetUser(id);

            if (user is not null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("users")]
        public async Task<IReadOnlyCollection<UserDto>> GetAllUsers()
        {
            return await _userManager.GetAllUsers();
        }

        [HttpPost("users")]
        public async Task<ActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = await _userManager.CreateUser(userDto);
            if (user is not null)
            {
                return Ok(user);
            }
            return BadRequest(user);
        }
    }
}
