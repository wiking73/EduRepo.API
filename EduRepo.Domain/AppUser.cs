using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace EduRepo.Domain
{
    public class AppUser : IdentityUser
    {
        public required string UserName { get; set; }

        public string Email { get; set; }

        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string NrAlbumu { get; set; }

        public ICollection<Uczestnictwo> Uczestnictwa { get; set; } = new List<Uczestnictwo>();

        public bool IsStudent { get; set; } = true;
        public bool IsTeacher { get; set; } = false;
    }
}
