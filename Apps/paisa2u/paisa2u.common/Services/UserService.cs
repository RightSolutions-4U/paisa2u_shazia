using Microsoft.EntityFrameworkCore;
using paisa2u.common.Resources;
using paisa2u.common.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace paisa2u.common.Services
{
    public sealed class UserService : IUserService
    {
        private readonly PaisaDbContext _context;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private IConfiguration configuration;

        public UserService(PaisaDbContext context, IConfiguration iConfig)
        {
            _context = context;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");
            /*this.configuration = configuration;*/
            configuration = iConfig;
        }

        public async Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken)
        { 
            var user = new Siteuser
            {
                Username = resource.Username,
                Email = resource.Email,
                PasswordSalt = PasswordHasher.GenerateSalt(),
                Adminrole = resource.adminrole,
                JwtToken = ""
            };
            user.PasswordHash = PasswordHasher.ComputeHash(resource.Pwd, user.PasswordSalt, _pepper, _iteration);
            await _context.Siteusers.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new UserResource(user.Userid.ToString(), user.Username,user.PasswordSalt, user.Email, user.JwtToken);
        }

        public async Task<UserResource> Login(LoginResource resource, CancellationToken cancellationToken)
        {
            var siteuser = await _context.Siteusers
                .FirstOrDefaultAsync(x => x.Email == resource.Email, cancellationToken);

            if (siteuser == null)
                throw new Exception("User does not exist.");

            var passwordHash = PasswordHasher.ComputeHash(resource.Pwd, siteuser.PasswordSalt, _pepper, _iteration);
            if (siteuser.PasswordHash != passwordHash)
                throw new Exception("Username or password did not match.");

            //Add JWT Token Code
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, siteuser.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                        /*new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName),*/
                        /*new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email)*/
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("JWT").GetSection("Key").Value
                ));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            siteuser.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserResource(siteuser.Username, siteuser.Email, "*", siteuser.Adminrole, siteuser.JwtToken );
        }
    }

}
