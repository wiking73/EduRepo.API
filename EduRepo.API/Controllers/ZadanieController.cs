using EduRepo.Domain;
using EduRepo.Application.Zadania;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EduRepo.API.Controllers
{
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
        
        public async Task<IActionResult> CreateZadanie([FromBody] CreateCommand command)
        {

            var result = await _mediator.Send(new CreateCommand { });
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPut("{id}")]
       
        public async Task<IActionResult> UpdateZadanie(int id, [FromBody] EditCommand command)
        {
            var result = await _mediator.Send(new EditCommand { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeleteZadanie(int id)
        {
            var result = await _mediator.Send(new DeleteCommand { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
