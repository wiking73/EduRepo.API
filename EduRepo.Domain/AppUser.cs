using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace EduRepo.Domain
{
    public class AppUser : IdentityUser
    {
        
        public required string UserName { get; set; }
        
       // public string Imie { get; set; }
        public string Email { get; set; }




        //  public ICollection<Kurs> KursyWlasne { get; set; }
        public ICollection<Uczestnictwo> Uczestnictwa { get; set; } = new List<Uczestnictwo>();

        //  public ICollection<Odpowiedz> Odpowiedzi { get; set; }
        // public ICollection<PowiadomienieBrakOdpowiedzi> Powiadomienia { get; set; }

        public bool IsStudent { get; set; } = true;
        public bool IsTeacher { get; set; } = false;
    }



}
