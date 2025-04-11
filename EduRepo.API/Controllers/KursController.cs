using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    public class KursController : BaseApiController
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Kurs>>> GetKurs()
        {
            return await _context.Kursy.ToListAsync();
        }
        [HttpGet("Kursy/{id}")]
        public async Task<ActionResult<Kurs>> GetKursy(Guid id)
        {
            return await _context.Kursy.FindAsync(id);
        }
    }
}
