/*using EduRepo.Application.Uczestnictwa;
using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UczestnictwoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public UczestnictwoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        public async Task<ActionResult<List<Uczestnictwo>>> GetUczestnictwa()
        {
            var query = new List.Query();  
            var uczestnictwa = await _mediator.Send(query);  
            return Ok(uczestnictwa);
        }
        [Authorize]

        [HttpGet("{id}")]
        public async Task<ActionResult<Uczestnictwo>> GetUczestnictwa(int id)
        {
            var result = await _mediator.Send(new Details.Query { Id = id });
            if (result == null) return NotFound();
            return result;
        }
        [Authorize]

        [HttpPost("{id}/dolacz")]
        public async Task<IActionResult> DolaczDoKursu(int id)
        {
            var userId = User.FindFirst("sub")?.Value ?? User.Identity.Name; 
            var UserName = User.Identity.Name; 

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Nie udało się pobrać identyfikatora użytkownika.");
            }

            var command = new ZapiszCommand
            {
                IdKursu = id,
                WlascicielId = userId,  
                UserName = UserName     
            };

            try
            {
                var uczestnictwo = await _mediator.Send(command);
                return Ok(uczestnictwo); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);  
            }
        }

    }
}
*/