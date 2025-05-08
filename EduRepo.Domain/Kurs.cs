using System.ComponentModel.DataAnnotations;

namespace EduRepo.Domain
{
    public class Kurs
    {
        [Key]
        public int IdKursu { get; set; }
        public string Nazwa { get; set; }
        public string OpisKursu { get; set; }
        public string RokAkademicki { get; set; }
        public string Klasa { get; set; }
        public bool CzyZarchiwizowany { get; set; }
        //public enum KursStatusDolaczenia { Potwierdzony, Odrzucony }
        public string WlascicielId{ get; set; }  // FK
        public string UserName { get; set; }
        public ICollection<Zadanie> Zadania { get; set; }
        public ICollection<Uczestnictwo> Uczestnicy { get; set; }

    }
}
