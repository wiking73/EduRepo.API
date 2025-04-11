using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    public class ZadanieController : BaseApiController
    {
        private readonly DataContext _context;
        public ZadanieController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Zadanie>>> GetZadanie()
        {
            return await _context.Zadania.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Zadanie>> GetZadania(Guid id)
        {
            return await _context.Zadania.FindAsync(id);
        }

    }
}
