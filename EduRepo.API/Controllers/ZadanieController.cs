using EduRepo.Domain;
using EduRepo.Application.Zadania;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EduRepo.API.Controllers
{
    [AllowAnonymous]
    public class ZadanieController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ZadanieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Zadanie>>> GetZadania()
        {
            return await _mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Zadanie>> GetZadanie(int id)
        {
            var result = await _mediator.Send(new Details.Query { Id = id });
            if (result == null) return NotFound();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> CreateZadanie([FromForm] CreateCommand command, IFormFile? zalacznik)
        {
            if (zalacznik != null && zalacznik.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{zalacznik.FileName}";
                var fullPath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await zalacznik.CopyToAsync(stream);

                command.PlikPomocniczy = $"/uploads/{fileName}";
            }
            else
            {
                command.PlikPomocniczy = null; // jawnie ustaw na null, jeśli nie ma pliku
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZadanie(int id, [FromBody] EditCommand command)
        {
            if (id != command.Id)
                command.Id = id;

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZadanie(int id)
        {
            var result = await _mediator.Send(new DeleteCommand { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadPlik(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nie wybrano pliku.");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativePath = $"/uploads/{uniqueFileName}";
            return Ok(new { path = relativePath });
        }
    }
}