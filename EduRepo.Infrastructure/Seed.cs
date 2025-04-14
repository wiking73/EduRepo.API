using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduRepo.Domain;

namespace EduRepo.Infrastructure
{
   public  class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Kursy.Any()) return;
            var kursy = new List<Kurs>
            {
                new Kurs
                {
                    Nazwa = "MathKurs",
                    OpisKursu = "Tak",
                    RokAkademicki = "2024",
                    Klasa = "3D",
                    CzyZarchiwizowany = true

                },
                new Kurs
                {
                    Nazwa = "AngKurs",
                    OpisKursu = "Tak",
                    RokAkademicki = "2023",
                    Klasa = "3D",
                    CzyZarchiwizowany = true

                }


            };
            await context.Kursy.AddRangeAsync(kursy);
            await context.SaveChangesAsync();
        }
    }
}
