using EduRepo.Domain;
using EduRepo.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduRepo.Application.Odpowiedzi;

namespace EduRepo.API.Controllers
{
    public class OdpowiedzController : BaseApiController
    {
        private readonly IMediator _mediator;

        public OdpowiedzController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Odpowiedz>>> GetOdpowiedzi()
        {
            return await _mediator.Send(new List.Query());
        }


/*        [HttpGet("{id}")]
        public async Task<ActionResult<Odpowiedz>> GetOdpowiedz(Guid id)
        {
            var result = await _mediator.Send(new List.Query { Id = id });
            if (result == null) return NotFound();
            return result;
        }*/
       /* [HttpPost]

        public async Task<IActionResult> CreateOdpowiedz([FromBody] CreateCommand command)
        {

            var result = await _mediator.Send(new CreateCommand { });
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateOdpowiedz(int id, [FromBody] EditCommand command)
        {
            var result = await _mediator.Send( new EditCommand { IdOdpowiedzi = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteOdpowiedz(int id)
        {
            var result = await _mediator.Send(new DeleteCommand { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }*/
    }
}

