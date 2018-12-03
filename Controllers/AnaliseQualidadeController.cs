using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Models;

namespace ProjetoFinal.Controllers
{
    public class AnaliseQualidadeController : Controller
    {
        private readonly ProjFinalContext _context;

        public AnaliseQualidadeController(ProjFinalContext context)
        {
            _context = context;
        }

        // GET: AnaliseQualidade
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnaliseQualidade
                .Include(ordem => ordem.ordem)
                .ToListAsync());
        }

        // GET: AnaliseQualidade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiseQualidade = await _context.AnaliseQualidade
                .Include(ordem => ordem.ordem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (analiseQualidade == null)
            {
                return NotFound();
            }

            return View(analiseQualidade);
        }

        // GET: AnaliseQualidade/Create
        public IActionResult Create()
        {
            ViewData["Ordem"] = new SelectList(_context.Ordem.ToList(), "Ordem").Items;
            return View();
        }

        // POST: AnaliseQualidade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,data,aprovado,responsavel")] AnaliseQualidade analiseQualidade, IFormCollection collection)
        {
            int idOrdem = Convert.ToInt16(collection["Ordem"]);
            if (ModelState.IsValid)
            {
                var ord = await _context.Ordem.FindAsync(idOrdem);
                analiseQualidade.ordem = ord;
               
                _context.Add(analiseQualidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(analiseQualidade);
        }

        // GET: AnaliseQualidade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiseQualidade = await _context.AnaliseQualidade.FindAsync(id);
            if (analiseQualidade == null)
            {
                return NotFound();
            }
            ViewData["Ordem"] = new SelectList(_context.Ordem.ToList(), "Ordem").Items;
            return View(analiseQualidade);
        }

        // POST: AnaliseQualidade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,data,aprovado,responsavel")] AnaliseQualidade analiseQualidade, IFormCollection collection)
        {
            int idOrdem = Convert.ToInt16(collection["Ordem"]);
            if (id != analiseQualidade.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ord = await _context.Ordem.FindAsync(idOrdem);
                    analiseQualidade.ordem = ord;
               
                    _context.Update(analiseQualidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnaliseQualidadeExists(analiseQualidade.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(analiseQualidade);
        }

        // GET: AnaliseQualidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiseQualidade = await _context.AnaliseQualidade
                .Include(ordem => ordem.ordem)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (analiseQualidade == null)
            {
                return NotFound();
            }

            return View(analiseQualidade);
        }

        // POST: AnaliseQualidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analiseQualidade = await _context.AnaliseQualidade.FindAsync(id);
            _context.AnaliseQualidade.Remove(analiseQualidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnaliseQualidadeExists(int id)
        {
            return _context.AnaliseQualidade.Any(e => e.ID == id);
        }
    }
}
