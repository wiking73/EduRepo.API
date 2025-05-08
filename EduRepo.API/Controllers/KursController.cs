using EduRepo.Domain;
using EduRepo.Application.Kursy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EduRepo.Infrastructure;

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
            return await _mediator.Send(new List.Query());
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
            var result = await _mediator.Send(new EditCommand { IdKursu = id });
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
        [HttpGet("{id}/uczestnicy")]
        public async Task<ActionResult<List<Uczestnictwo>>> GetUczestnicy(int id)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.Identity.Name;

            var query = new EduRepo.Application.Uczestnictwa.List.Query
            {
                UserId = userId
            };

            var uczestnicy = await _mediator.Send(query);

            if (uczestnicy == null || uczestnicy.Count == 0)
            {
                return NotFound("Nie znaleziono żadnych uczestników.");
            }

            return Ok(uczestnicy);
        }

    }
}
