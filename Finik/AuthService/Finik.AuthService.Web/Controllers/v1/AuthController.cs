using Asp.Versioning;
using Finik.AuthService.Contracts;
using Finik.AuthService.Core;
using Finik.AuthService.Web.Attributes;
using Finik.AuthService.Web.Extensions;
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
        private readonly IUserService _userManager;
        private readonly IAuthManager _authManager;
        private readonly IPasswordManager _passwordManager;

        public AuthController(IUserService userManager, IAuthManager authManager, IPasswordManager passwordManager)
        {
            _userManager = userManager;
            _authManager = authManager;
            _passwordManager = passwordManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userManager.GetUser(loginRequest.Email);

            if (user != null && _passwordManager.IsPasswordValid(loginRequest.Password, user.HashedPassword)) 
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
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            var user = await _userManager.GetUser(createUserRequest.Email);
            if (user is null)
            {
                var hashedPassword = _passwordManager.HashPassword(createUserRequest.Password);
                user = await _userManager.CreateUser(createUserRequest.ToDto(hashedPassword));

                if (user is not null)
                {
                    return Ok(user);
                }
            }
            return BadRequest();
        }
    }
}
