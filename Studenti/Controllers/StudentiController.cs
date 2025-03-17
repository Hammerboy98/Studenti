using Microsoft.AspNetCore.Mvc;
using Studenti.Data;
using Studenti.Models;
using Microsoft.EntityFrameworkCore;

namespace Studenti.Controllers
{
    public class StudentiController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var studenti = await _context.Studenti.ToListAsync();
            return View(studenti);
        }

        public async Task<IActionResult> Dettagli(int id)
        {
            var studente = await _context.Studenti.FindAsync(id);
            if (studente == null) return NotFound();
            return PartialView("_Dettagli", studente);
        }

        public async Task<IActionResult> Modifica(int id)
        {
            var studente = await _context.Studenti.FindAsync(id);
            if (studente == null) return NotFound();
            return PartialView("_Modifica", studente);
        }

        [HttpPost]
        public async Task<IActionResult> Modifica(Studente studente)
        {
            if (ModelState.IsValid)
            {
                _context.Update(studente);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return PartialView("_Modifica", studente);
        }
    }
}
