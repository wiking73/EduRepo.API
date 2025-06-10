using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduRepo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadController(DataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        [Route("upload")]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] int idZadania)
        {
            Console.WriteLine("UPLOAD: Start");

            if (file == null || file.Length == 0)
            {
                Console.WriteLine("UPLOAD: Plik pusty.");
                return BadRequest("Plik jest pusty.");
            }

            var zadanie = await _context.Zadania.FindAsync(idZadania);
            if (zadanie == null)
            {
                Console.WriteLine("UPLOAD: Zadanie nie istnieje.");
                return NotFound("Zadanie nie istnieje.");
            }

            var kurs = await _context.Kursy
                .Include(k => k.Wlasciciel)
                .FirstOrDefaultAsync(k => k.IdKursu == zadanie.IdKursu);
            if (kurs == null)
            {
                Console.WriteLine("UPLOAD: Kurs nie istnieje.");
                return NotFound("Kurs nie istnieje.");
            }

            var studentImie = User.FindFirstValue(ClaimTypes.GivenName);
            var studentNazwisko = User.FindFirstValue(ClaimTypes.Surname);
            var studentAlbum = User.FindFirst("albumNumber")?.Value;

            Console.WriteLine($"UPLOAD: JWT -> {studentImie}, {studentNazwisko}, {studentAlbum}");

            if (studentImie == null || studentNazwisko == null || studentAlbum == null)
            {
                Console.WriteLine("UPLOAD: Dane z tokena są null.");
                return Unauthorized("Brak wymaganych danych w tokenie.");
            }

            var kursFolder = $"{kurs.Wlasciciel.Nazwisko}_{kurs.Nazwa}_{kurs.RokAkademicki}";
            var studentFolder = $"{studentNazwisko}_{studentImie}_{studentAlbum}";
            var zadanieFolder = zadanie.Nazwa;

            var folderPath = Path.Combine(_env.ContentRootPath, "Uploads", kursFolder, studentFolder, zadanieFolder);
            Console.WriteLine($"UPLOAD: folderPath = {folderPath}");

            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, file.FileName);
            Console.WriteLine($"UPLOAD: filePath = {filePath}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            Console.WriteLine("UPLOAD: Plik zapisany.");

            return Ok(new { message = "Plik został zapisany.", path = filePath });
        }
    }
}
