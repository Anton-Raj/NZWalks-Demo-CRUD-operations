using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        //Used to get key from appsettings.json
        private readonly IConfiguration configuration;

        public LoginRepository(IConfiguration config)
        {
            this.configuration = config;
        }
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials : credentials
                ) ;

            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }
    }
}


