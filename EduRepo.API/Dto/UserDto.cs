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
        public string UserName { get; set; }
    }
}
