using EduRepo.Domain;
using EduRepo.Application.Kursy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EduRepo.Infrastructure;
using System.Security.Claims;
using EduRepo.Application.Uczestnictwa;
using KursyList = EduRepo.Application.Kursy.List;

namespace EduRepo.API.Controllers
{
    [AllowAnonymous]
    public class KursController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly DataContext _context;

        public KursController(IMediator mediator, DataContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kurs>>> GetKursy()
        {
            return await _mediator.Send(new KursyList.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kurs>> GetKurs(int id)
        {
            var kurs = await _context.Kursy
                .Include(k => k.Zadania)
                .FirstOrDefaultAsync(k => k.IdKursu == id);

            if (kurs == null)
                return NotFound();

            return Ok(kurs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKurs([FromBody] CreateCommand command)
        {
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKurs(int id, [FromBody] EditCommand command)
        {
            command.IdKursu = id;
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKurs(int id)
        {
            try
            {
                await _mediator.Send(new DeleteCommand { Id = id });
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost("{id}/dolacz")]
        public async Task<IActionResult> DolaczDoKursu(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Nie można znaleźć ID użytkownika.");

            var command = new ZapiszCommand
            {
                KursId = id,
                UserId = userId
            };

            try
            {
                await _mediator.Send(command);
                return Ok("Zapisano zgłoszenie.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ✅ Lista oczekujących zgłoszeń
        [Authorize]
        [HttpGet("{id}/prosby")]
        public async Task<IActionResult> GetPendingRequests(int id)
        {
            var kurs = await _context.Kursy
                .Include(k => k.Uczestnicy)
                .ThenInclude(u => u.Wlasciciel)
                .FirstOrDefaultAsync(k => k.IdKursu == id);

            if (kurs == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (kurs.WlascicielId != userId)
                return Forbid("Nie jesteś właścicielem tego kursu.");

            var prosby = kurs.Uczestnicy
                .Where(u => u.Status == StatusUczestnika.Oczekuje)
                .Select(u => new {
                    u.IdUczestnictwa,
                    u.UserName,
                    u.WlascicielId,
                    u.Status
                });

            return Ok(prosby);
        }

        // ✅ Akceptacja zgłoszenia
        [Authorize]
        [HttpPut("{kursId}/zaakceptuj/{uczestnictwoId}")]
        public async Task<IActionResult> AcceptRequest(int kursId, int uczestnictwoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var uczestnictwo = await _context.Uczestnictwa
                .FirstOrDefaultAsync(u => u.IdUczestnictwa == uczestnictwoId && u.KursId == kursId);

            if (uczestnictwo == null)
                return NotFound();

            var kurs = await _context.Kursy.FindAsync(kursId);
            if (kurs.WlascicielId != userId)
                return Forbid();

            uczestnictwo.Status = StatusUczestnika.Zaakceptowano;
            await _context.SaveChangesAsync();

            return Ok("Zaakceptowano zgłoszenie.");
        }

        // ✅ Odrzucenie zgłoszenia
        [Authorize]
        [HttpPut("{kursId}/odrzuc/{uczestnictwoId}")]
        public async Task<IActionResult> RejectRequest(int kursId, int uczestnictwoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var uczestnictwo = await _context.Uczestnictwa
                .FirstOrDefaultAsync(u => u.IdUczestnictwa == uczestnictwoId && u.KursId == kursId);

            if (uczestnictwo == null)
                return NotFound();

            var kurs = await _context.Kursy.FindAsync(kursId);
            if (kurs.WlascicielId != userId)
                return Forbid();

            uczestnictwo.Status = StatusUczestnika.Odrzucono;
            await _context.SaveChangesAsync();

            return Ok("Odrzucono zgłoszenie.");
        }

        [Authorize]
        [HttpGet("{id}/uczestnicy")]
        public async Task<IActionResult> GetAcceptedParticipants(int id)
        {
            var kurs = await _context.Kursy
                .Include(k => k.Uczestnicy)
                .ThenInclude(u => u.Wlasciciel)
                .FirstOrDefaultAsync(k => k.IdKursu == id);

            if (kurs == null)
                return NotFound("Kurs nie istnieje");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (kurs.WlascicielId != userId)
                return Forbid("Nie jesteś właścicielem tego kursu.");

            var uczestnicy = kurs.Uczestnicy
                .Where(u => u.Status == StatusUczestnika.Zaakceptowano)
                .Select(u => new {
                    u.IdUczestnictwa,
                    u.UserName,
                    u.WlascicielId
                });

            return Ok(uczestnicy);
        }

        [Authorize]
        [HttpDelete("{kursId}/usunuczestnika/{uczestnictwoId}")]
        public async Task<IActionResult> RemoveParticipant(int kursId, int uczestnictwoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var uczestnictwo = await _context.Uczestnictwa
                .FirstOrDefaultAsync(u => u.IdUczestnictwa == uczestnictwoId && u.KursId == kursId);

            if (uczestnictwo == null)
                return NotFound("Nie znaleziono uczestnika.");

            var kurs = await _context.Kursy.FindAsync(kursId);
            if (kurs == null || kurs.WlascicielId != userId)
                return Forbid("Brak uprawnień.");

            _context.Uczestnictwa.Remove(uczestnictwo);
            await _context.SaveChangesAsync();

            return Ok("Uczestnik został usunięty z kursu.");
        }

        [Authorize]
        [HttpGet("{username}/mojekursy")]
        public async Task<IActionResult> GetMyCourses(string username)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var kursy = await _context.Uczestnictwa
                .Where(u => u.UserName == username)
                .Where(u => u.Status == StatusUczestnika.Zaakceptowano)
                .Select(u => new
                {
                    u.KursId,
                    u.UserName
                })
                .ToListAsync();
            //.Where(u => u.UserId == userId)
            //.Where(u => u.Status == StatusUczestnika.Zaakceptowano)
            //.Include(k => k.Kurs)
            //.Select(u => u.Kurs).ToListAsync();
            Console.WriteLine(userId);


            //var kurs = await _context.Uczestnictwa
            //    .Include(k => k.UserName)
            //    .ToListAsync();
            //if (kurs == null)
            //    return NotFound("Kurs nie istnieje");

            return Ok(kursy);


            //var uczestnicy = kurs.Uczestnicy
            //    .Where(u => u.Status == StatusUczestnika.Zaakceptowano)
            //    .Select(u => new {
            //        u.IdUczestnictwa,
            //        u.UserName,
            //        u.WlascicielId
            //    });

        }
    }
}
