using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace EduRepo.Domain
{
    public class AppUser : IdentityUser
    {
        
        
        public string UserName { get; set; }
        
       // public string Imie { get; set; }
        public string Email { get; set; }
         
        //  public string Klasa { get; set; }

        //  public ICollection<Kurs> KursyWlasne { get; set; }
        ////  public ICollection<Uczestnictwo> Uczestnictwa { get; set; }
        //  public ICollection<Odpowiedz> Odpowiedzi { get; set; }
        // public ICollection<PowiadomienieBrakOdpowiedzi> Powiadomienia { get; set; }

        public bool IsStudent { get; set; } = false;
    }

   /* public class Uczestnictwo
    {
        [Key]
        public int IdKursu { get; set; }
        public Kurs Kurs { get; set; }

        public int IdUzytkownika { get; set; }
        public Uzytkownik Uzytkownik { get; set; }

        public string Status { get; set; }
    }*/

}
