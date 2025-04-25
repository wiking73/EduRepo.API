using System.ComponentModel.DataAnnotations;

namespace EduRepo.Domain
{
    public class Odpowiedz
    {
        [Key]
        public int IdOdpowiedzi { get; set; }
        public int IdZadania { get; set; }
        public string KomentarzNauczyciela { get; set; } = "brak"; 
        public string Ocena { get; set; } = "brak";
        public string NazwaPliku { get; set; }
        public DateTime DataOddania { get; set; }
        public string WlascicielId { get; set; }
        public string UserName { get; set; }
    }

}
