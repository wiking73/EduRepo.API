using EduRepo.Domain;
using EduRepo.Application.Kursy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EduRepo.API.Controllers
{
    public class KursController : BaseApiController
    {
        private readonly IMediator _mediator;

        public KursController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<Kurs>>> GetKursy()
        {
            return await _mediator.Send(new List.Query());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Kurs>> GetKurs(int id)
        {
            var result = await _mediator.Send(new Details.Query { Id = id });
            if (result == null) return NotFound();
            return result;
        }
     // [HttpPost]
        public async Task<IActionResult> CreateKurs([FromBody] CreateCommand command)
        {
            var result = await _mediator.Send(new CreateCommand { });
            if (result == null) return NotFound();
            return Ok(result);
        }

        //[HttpPut("{id}")]
        /*

        public async Task<IActionResult> UpdateKurs(int id, [FromBody] EditCommand command)
        {
            var result = await _mediator.Send(new EditCommand { IdKursu = id });
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteKurs(int id)
        {
            var result = await _mediator.Send(new DeleteCommand { Id = id });
            if (result == null) return NotFound();
            return Ok(result);
        }*/
    }
}
