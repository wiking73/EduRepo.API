using Microsoft.EntityFrameworkCore;
using EduRepo.Domain;


namespace Bikes.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options) { }

        public DbSet<Kurs> Kursy { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Odpowiedz> Odpowiedzi { get; set; }
        public DbSet<Powiadomienie> Powiadomienia { get; set; }
        public DbSet<Zadanie> Zadania { get; set; }
        public DbSet<Uczestnictwo> Uczestnictwa { get; set; }
    }
        
}
}
