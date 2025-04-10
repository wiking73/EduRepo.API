namespace EduRepo.Domain
{
    public class Kurs
    {
        public int IdKursu { get; set; }
        public string Nazwa { get; set; }
        public string OpisKursu { get; set; }
        public string RokAkademicki { get; set; }
        public string Klasa { get; set; }
        public bool CzyZarchiwizowany { get; set; }

        public int IdWlasciciela { get; set; }
        public Uzytkownik Wlasciciel { get; set; }
        public ICollection<Uczestnictwo> Uczestnicy { get; set; }
        public ICollection<Zadanie> Zadania { get; set; }

    }
}
