using Microsoft.EntityFrameworkCore;
using EduRepo.Domain;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EduRepo.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
             : base(options) { }

        public DbSet<Kurs> Kursy { get; set; }
       // public DbSet<AppUser> Uzytkownicy { get; set; }
        public DbSet<Odpowiedz> Odpowiedzi { get; set; }
        public DbSet<PowiadomienieBrakOdpowiedzi> Powiadomienia { get; set; }
        public DbSet<Zadanie> Zadania { get; set; }

      
        // public DbSet<Uczestnictwo> Uczestnictwa { get; set; }
    }


}
