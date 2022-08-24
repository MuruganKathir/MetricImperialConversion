using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Conversion.API.Policies;
using Conversion.Core.ApiModels;
using Conversion.DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Conversion.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                if (!user.EmailConfirmed)
                {
                    var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmResult = await _userManager.ConfirmEmailAsync(user, confirmToken);
                }


                var token = await GenerateJwtToken(user);

                return Ok(new
                {
                    token
                });
            }
            return BadRequest("Invalid username or password");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "User already exists!" });

            var user = new ApplicationUser
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register-admin")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRequest request)
        {
            var userExists = await _userManager.FindByNameAsync(request.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "User already exists!" });

            var user = new ApplicationUser
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));


            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok();
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var claimsIdentity = new ClaimsIdentity(claims);

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.Now.AddHours(3),
                Subject = claimsIdentity,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}