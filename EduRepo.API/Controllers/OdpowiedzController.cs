using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    public class OdpowiedzController : BaseApiController
    {
        private readonly DataContext _context;
        public OdpowiedzController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Odpowiedz>>> GetOdpowiedz()
        {
            return await _context.Odpowiedzi.ToListAsync();
        }
        [HttpGet("Odpowiedz/{id}")]

        public async Task<ActionResult<Odpowiedz>> GetOdpowiedzi(Guid id)
        {
            return await _context.Odpowiedzi.FindAsync(id);
        }
    }
}
