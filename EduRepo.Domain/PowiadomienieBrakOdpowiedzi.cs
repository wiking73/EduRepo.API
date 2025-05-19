using System.ComponentModel.DataAnnotations;

namespace EduRepo.Domain
{
    public class PowiadomienieBrakOdpowiedzi
    {
        [Key]
        public int IdPowiadomienia { get; set; }
        public int IdZadania { get; set; }
        //public Zadanie Zadanie { get; set; }

        //  public int IdUzytkownika { get; set; }
        // public Uzytkownik Uzytkownik { get; set; }

        public DateTime DataPowiadomienia { get; set; }
        public bool CzyOdczytane { get; set; }
    }
}