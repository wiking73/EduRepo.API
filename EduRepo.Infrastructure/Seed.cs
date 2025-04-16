using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduRepo.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace EduRepo.Infrastructure
{
   public  class Seed
    {
        public static async Task SeedData(DataContext context, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
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
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        UserName = "admin",
                        Email = "admin@example.com",

                        IsStudent = true,

                    },
                    new AppUser
                    {
                        UserName = "user",
                        Email = "mail@SD",

                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Zaq12wsx");
                    
                }
            }


            await context.Kursy.AddRangeAsync(kursy);
            await context.SaveChangesAsync();
        }
    }
}
