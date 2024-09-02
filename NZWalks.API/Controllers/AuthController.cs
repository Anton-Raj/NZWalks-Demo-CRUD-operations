using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        private readonly ILoginRepository loginRepository;
        public AuthController(UserManager<IdentityUser> usermanager,ILoginRepository repository)
        {
            this.userManager = usermanager;
            this.loginRepository = repository;
        }

        //POST : /api/auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = requestDto.Username,
                Email = requestDto.Email,
            };

            var identityResult = await userManager.CreateAsync(identityUser,requestDto.Password);

            if(identityResult.Succeeded) 
            {
                if(requestDto.Roles != null && requestDto.Roles.Any()) 
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, requestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User created Successfully! Please login again");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        //POST :/api/auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if(user != null) 
            {
                var checkPassword = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if(checkPassword)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    //Create a token
                    var jwtToken = loginRepository.CreateJwtToken(user,roles.ToList());

                    var response = new LoginResponseDto
                    {
                        JwtToken = jwtToken,
                    };
                    return Ok(response);
                }
            }

            return BadRequest("Email or password is incorrect");
        }
    }
}
