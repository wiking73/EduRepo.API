/*using EduRepo.Domain;
using EduRepo.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduRepo.API.Controllers
{
    public class UzytkownikController : BaseApiController
    {
        private readonly DataContext _context;
        public UzytkownikController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Uzytkownik>>> GetUzytkownik()
        {
            return await _context.Uzytkownicy.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Uzytkownik>> GetUzytkownicy(Guid id)
        {
            return await _context.Uzytkownicy.FindAsync(id);
        }
      
       
    }
}
*/