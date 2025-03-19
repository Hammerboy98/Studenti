using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Studenti.Data;
using Studenti.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; // Aggiungi questo namespace

namespace Studenti.Controllers
{
    [Authorize] // Questa linea protegge tutto il controller
    public class StudentiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StudentiController> _logger;

        public StudentiController(ApplicationDbContext context, ILogger<StudentiController> logger)
        {
            _context = context;
            _logger = logger;
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

        public IActionResult Crea()
        {
            return PartialView("_Crea", new Studente());
        }

        [HttpPost]
        public async Task<IActionResult> Crea(Studente studente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studente);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Studente creato: {Nome} {Cognome}", studente.Nome, studente.Cognome);
                return Json(new { success = true });
            }
            return PartialView("_Crea", studente);
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
                _logger.LogInformation("Studente modificato: {Nome} {Cognome}", studente.Nome, studente.Cognome);
                return Json(new { success = true });
            }
            return PartialView("_Modifica", studente);
        }

        [HttpPost]
        public async Task<IActionResult> Elimina(int id)
        {
            var studente = await _context.Studenti.FindAsync(id);
            if (studente == null) return NotFound();

            _context.Studenti.Remove(studente);
            await _context.SaveChangesAsync();
            _logger.LogWarning("Studente eliminato: {Nome} {Cognome}", studente.Nome, studente.Cognome);

            return Json(new { success = true });
        }
    }
}
