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

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


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
                IdKursu = id,
                UserId = userId // backend sam ustawia
            };

            await _mediator.Send(command);
            return Ok("Zapisano zgłoszenie.");
        }



    }
}
