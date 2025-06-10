using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BCrypt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using EduRepo.API.Controllers;
using System.ComponentModel.DataAnnotations;
using EduRepo.Infrastructure;
using EduRepo.API.Dto;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNet.Identity;

namespace EduRepo.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHasher<AppUser> _passwordHasher;

        // Konstruktor z wstrzyknięciem zależności
        public AuthController(DataContext context, TokenService tokenService, PasswordHasher<AppUser> passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Sprawdzanie, czy email już istnieje
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                return BadRequest(new { message = "Email już istnieje." });

            // Sprawdzanie, czy nazwa użytkownika już istnieje
            if (await _context.Users.AnyAsync(u => u.UserName == registerDto.Username))
                return BadRequest(new { message = "Nazwa użytkownika już istnieje." });

            // Hashowanie hasła
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Tworzenie nowego użytkownika
            var user = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Imie = registerDto.Imie,
                Nazwisko = registerDto.Nazwisko,
                NrAlbumu = registerDto.NrAlbumu,
                IsStudent = true // lub false – zależnie od logiki rejestracji
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generowanie tokena
            var token = _tokenService.CreateToken(user);

            // Zwracanie odpowiedzi z tokenem i nazwą użytkownika
            return Ok(new
            {
                token,
                username = user.UserName
            });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // Zmieniono z UserName na Email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Nieprawidłowy email");

            // Weryfikacja hasła za pomocą BCrypt
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
                return Unauthorized("Nieprawidłowe hasło");

            // Generowanie tokena
            var token = _tokenService.CreateToken(user);

            return Ok(new { Token = token, Username = user.UserName });
        }


        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            // Pobranie emaila użytkownika z tokena JWT
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Użytkownik nie jest uwierzytelniony.");
            }

            // Pobranie użytkownika z bazy danych na podstawie emaila z tokenu
            var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika.");
            }

            // Przygotowanie odpowiedzi z profilem użytkownika
            var userProfile = new
            {
                Username = user.UserName,
                Email = user.Email,
                
            };

            return Ok(userProfile);
        }

        [HttpPost("change-password-login")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Użytkownik nie jest uwierzytelniony.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.PasswordHash);
            if (!isPasswordValid)
            {
                return BadRequest("Podane obecne hasło jest nieprawidłowe.");
            }

            var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);

            user.PasswordHash = newPasswordHash;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok("Hasło zostało pomyślnie zmienione.");
        }






        [HttpDelete("delete-account")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Użytkownik nie jest uwierzytelniony.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika.");
            }

            // Usunięcie użytkownika
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Konto zostało usunięte.");
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.IsTeacher,
                    u.IsStudent,
                })
                .ToListAsync();

            return Ok(users);
        }
        

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.IsTeacher,
                    u.IsStudent
                })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika.");
            }

            return Ok(user);
        }

        [HttpPut("user/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto request)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika.");
            }

            user.UserName = request.UserName;
            user.Email = request.Email;
            user.IsTeacher = request.IsTeacher;
            user.IsStudent = request.IsStudent;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.IsTeacher,
                user.IsStudent
            });
        }





    }
}


   


