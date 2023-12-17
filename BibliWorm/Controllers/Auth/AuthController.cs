using BLL.Contracts;
using BLL.Infrastructure.Models.User;
using Common.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPI.Infrastructure.Extensions;
using WebAPI.Infrastructure.Models;

namespace MangaHub.Controllers.IdentityServer
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthOptions _authOptions;

        private readonly IUserService _userService;

        public AuthController(
            AuthOptions authOptions,
            IUserService userService)
        {
            _authOptions = authOptions;
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userService.LoginUser(model.Login, model.Password);
            var token = GetToken(user);

            return Ok(token);
        }

        [HttpGet("refresh")]
        public ActionResult Refresh([FromHeader] string refreshTokenString)
        {
            var refreshToken = refreshTokenString.DecodeToken();
            var user = _userService.GetUserByRefreshToken(refreshToken);
            var token = GetToken(user);

            return Ok(token);
        }

        #region Helpers

        private Token GetToken(UserModel user)
        {
            var credentials = new SigningCredentials(
                key: _authOptions.PrivateKey,
                algorithm: SecurityAlgorithms.RsaSha256
            );

            var jwtToken = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                new List<Claim>() {
                    new Claim("id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                },
                expires: DateTime.UtcNow.AddSeconds(_authOptions.TokenLifetime),
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var token = new Token()
            {
                AccessToken = tokenString,
                RefreshToken = _userService.CreateRefreshToken(user.UserId).EncodeToken()
            };

            return token;
        }

        #endregion
    }
}
