using Crosscutting.Identity.RequestModels;
using Domain.Model.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwTSettings _jwTSettings;

        public AuthController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwTSettings> jwtSettingsOptionsMonitor)
        {
            _userManager = userManager;
            _jwTSettings = jwtSettingsOptionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> GetToken([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult(new { Message = "Invalid user or password" });

            var identityUser = await GetIdentityUser(loginRequest);
            if (identityUser == null)
                return new BadRequestObjectResult(new { Message = "Unknown user or password" });

            var token = GenerateToken(identityUser);

            return Ok(new { Token = token, Message = "Success" });
        }

        private object GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.Email, identityUser.Email),
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwTSettings.SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddSeconds(_jwTSettings.ExpiryTimeInSeconds),
                Audience = _jwTSettings.Audience,
                Issuer = _jwTSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<IdentityUser> GetIdentityUser(LoginRequest loginRequest)
        {
            var identityUser = await _userManager.FindByEmailAsync(loginRequest.User);
            if (identityUser == null)
                return null;

            var passwordVerificationResult = _userManager.PasswordHasher
                .VerifyHashedPassword(identityUser, identityUser.PasswordHash, loginRequest.Password);

            return passwordVerificationResult == PasswordVerificationResult.Failed ? null : identityUser;
        }
    }
}
