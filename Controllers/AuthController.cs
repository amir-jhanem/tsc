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
using TSC.Controllers.Resources;
using TSC.Core;
using TSC.Core.Models;

namespace TSC.Controllers
{
    [Route("api/AuthController")]
    public class AuthController:Controller
    {
        private readonly IGroupRepository groupRepository;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AuthController(
            IGroupRepository groupRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            IConfiguration configuration)
        {
            this.groupRepository = groupRepository;
            _userManager = userManager;
            _singInManager = signInManager;
            _roleManager = roleManager;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/AuthController/Login
        public async Task<IActionResult> UserLogin([FromBody]ApplicationUserResource model)
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
        public async Task<IActionResult> UserRegister([FromBody]ApplicationUserResource model)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var applicationUser = new ApplicationUser() {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };
            await InitialRoles();

            var result = await _userManager.CreateAsync(applicationUser, model.Password);

            if(result.Succeeded){
                await _userManager.AddToRoleAsync(applicationUser, model.IsAdmin ? "Admin":"Moderator");
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
        [HttpPost]
        [Route("AddMemberGroup")]
        public IActionResult AddMemberGroup([FromBody] UserGroupResource model)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            groupRepository.AddMemberGroup(model.UserId,model.GroupId);
            unitOfWork.CompleteAsync();
            return Ok(model);
        }

        public async Task InitialRoles(){

            string[] roleNames = {"Admin", "Moderator"};
            IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        //create the roles and seed them to the database: Question 1
                        roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
        }   
    }
}