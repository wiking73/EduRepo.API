using System.ComponentModel.DataAnnotations;

namespace EduRepo.Domain
{
    public class Zadanie
    {
        [Key]
        public int IdZadania { get; set; }
        public int IdKursu { get; set; }
        public Kurs Kurs { get; set; }

        public string Nazwa { get; set; }
        public string Tresc { get; set; }
        public DateTime TerminOddania { get; set; }
        public string PlikPomocniczy { get; set; }
        public bool CzyObowiazkowe { get; set; }

        public ICollection<Odpowiedz> Odpowiedzi { get; set; }
        public ICollection<PowiadomienieBrakOdpowiedzi> Powiadomienia { get; set; }

    }
}
