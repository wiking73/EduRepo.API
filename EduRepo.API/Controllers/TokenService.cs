using System.Security.Claims;
using System.Text;
using EduRepo.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace EduRepo.API.Controllers
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        // Konstruktor wstrzykujący konfigurację
        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role,
            user.IsStudent ? "Student" :
            user.IsTeacher ? "Teacher" :
            "Admin"),
        new Claim(ClaimTypes.GivenName, user.Imie),
        new Claim(ClaimTypes.Surname, user.Nazwisko),
        new Claim("albumNumber", user.NrAlbumu)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}