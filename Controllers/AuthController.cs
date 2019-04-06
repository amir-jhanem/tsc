using System;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TSC.Core.Models;

namespace TSC.Controllers
{
    [Route("api/AuthController")]
    public class AuthController:Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private readonly IConfiguration configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/AuthController/Login
        public async Task<Object> UserLogin([FromBody]ApplicationUserModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user != null && await _userManager.CheckPasswordAsync(user,model.Password))
            {
                var claims = new []
                {
                    new Claim (JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("signingKey").ToString()));
                var token = new JwtSecurityToken(
                    issuer:configuration.GetSection("issuer").ToString(),
                    audience:configuration.GetSection("audience").ToString(),
                    expires : DateTime.UtcNow.AddHours(1),
                    claims:claims,
			        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("Register")]
        //POST : /api/AuthController/Register
        public async Task<Object> UserRegister([FromBody]ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser() {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}