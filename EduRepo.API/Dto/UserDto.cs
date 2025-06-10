using System.ComponentModel.DataAnnotations;

namespace EduRepo.API.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Token { get; set; }
    }
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NrAlbumu { get; set; }
    }
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        //[MinLength(6, ErrorMessage = "Nowe hasło musi mieć co najmniej 6 znaków.")]
        public string NewPassword { get; set; }
    }
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
    }

}
