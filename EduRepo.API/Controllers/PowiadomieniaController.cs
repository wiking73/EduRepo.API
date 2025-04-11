using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    public class PowiadomienieController : BaseApiController
    {
        private readonly DataContext _context;
        public PowiadomienieController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PowiadomienieBrakOdpowiedzi>>> GetPowiadomienie()
        {
            return await _context.Powiadomienia.ToListAsync();
        }
        [HttpGet("Powiadomienia/{id}")]
        public async Task<ActionResult<PowiadomienieBrakOdpowiedzi>> GetPowiadomienia(Guid id)
        {
            return await _context.Powiadomienia.FindAsync(id);
        }
    }
}
