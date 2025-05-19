using EduRepo.Domain;
using EduRepo.Application.Zadania;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EduRepo.API.Controllers
{
    public class PowiadomieniaController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PowiadomieniaController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Zadanie>>> GetPowiadomienia()
        {
            return await _mediator.Send(new List.Query());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Zadanie>> GetPowiadomienie(int id)
        {
            var result = await _mediator.Send(new Details.Query { Id = id });
            if (result == null) return NotFound();
            return result;
        }
        [HttpPost]


        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePowiadomienie(int id)
        {
            var result = await _mediator.Send(new DeleteCommand { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}