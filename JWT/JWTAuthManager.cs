
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using VulnerableAPIProject.Entities.Base;
using VulnerableAPIProject.JWT;


namespace VulnerableAPIProject.JWT
{
     public class JWTAuthManager
    {
        private readonly IJWTSettings _config;

        public JWTAuthManager(IJWTSettings config)
        {
            _config = config;
        }

        public string GenerateTokens(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_config.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {

                    new Claim(ClaimTypes.NameIdentifier, account.firstName),
                    new Claim(ClaimTypes.Email, account.email),
                    new Claim(ClaimTypes.Role, account.role),
                    
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string TakeEmailfromJWT(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var JWTClaims = handler.ReadJwtToken(token).Claims;
                var email = JWTClaims.FirstOrDefault(c => c.Type == "email").Value;

                return email;
            }
            catch
            {
                return null;
            }
        }

        public string TakeFirstNamefromJWT(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var JWTClaims = handler.ReadJwtToken(token).Claims;
                var firstName = JWTClaims.FirstOrDefault(n => n.Type == "firstName").Value;

                return firstName;
            }
            catch
            {
                return null;
            }
        }

        public string TakeRolefromJWT(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var JWTClaims = handler.ReadJwtToken(token).Claims;
                var role = JWTClaims.FirstOrDefault(r => r.Type == "Role").Value;

                return role;
            }
            catch
            {
                return null;
            }
        }


    }

    } 


   

