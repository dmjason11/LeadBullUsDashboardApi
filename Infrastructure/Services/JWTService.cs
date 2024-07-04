using Core.Identity;
using Core.IServices;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey key;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
            var secretKey = _configuration.GetSection("JwtSetting:JwtKey").Value;
            key = new SymmetricSecurityKey(Encoding.UTF8
                   .GetBytes(secretKey));
        }
        public string GetJWT(AppUser user)
        {
            var claims = new Claim[]
           {
                 new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                 new Claim(JwtRegisteredClaimNames.Email,user.Email),
                 new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)
           };
            var signingCredintiels = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration.GetSection("JwtSetting:ExpiresInDays").Value)),
                SigningCredentials = signingCredintiels,
                Issuer = _configuration.GetSection("JwtSetting:Issuer").Value
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
