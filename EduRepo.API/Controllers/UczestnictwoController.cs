/*using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    public class UczestnictwoController : BaseApiController
    {
        private readonly DataContext _context;
        public UczestnictwoController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Uczestnictwo>>> GetUczestnictwo()
        {
            return await _context.Uczestnictwa.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Uczestnictwo>> GetUczestnictwa(Guid id)
        {
            return await _context.Uczestnictwa.FindAsync(id);
        }
    }
}
*/