using Microsoft.AspNetCore.Mvc;
using MovieHub.API.Services.Interfaces;
using MovieHub.API.Models.Auth;

namespace MovieHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtTokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthController(IJwtTokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var userExists = await _authService.ValidateGoogleUserAsync(request.Email, request.GoogleId);

            if (!userExists)
                return Unauthorized("User not registered");

            var token = _tokenService.GenerateToken(request.Email);

            return Ok(new { Token = token });
        }

    }
}
