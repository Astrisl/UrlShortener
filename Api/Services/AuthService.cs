using Data.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services
{
    public class AuthService
    {
        private readonly string _tokenSecurityKeyString;

        public AuthService(string tokenSecurityKeyString)
        {
            _tokenSecurityKeyString = tokenSecurityKeyString;
        }

        public void CreatePasswordHash(string password, out string hash, out string salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = Convert.ToBase64String(hmac.Key);
                hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.ASCII.GetBytes(password)));
            }
        }

        public bool VerifyPasswordHash(string password, string hash, string salt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(salt)))
            {
                string computedHash = Convert.ToBase64String(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
                return string.Equals(computedHash, hash);
            }
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Name)
            };

            SymmetricSecurityKey key = new(
                Encoding.UTF8.GetBytes(_tokenSecurityKeyString));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
