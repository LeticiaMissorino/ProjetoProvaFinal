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
    public class OrdemController : Controller
    {
        private readonly ProjFinalContext _context;

        public OrdemController(ProjFinalContext context)
        {
            _context = context;
        }

        // GET: Ordem
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ordem
            .Include(cliente => cliente.cliente)
            .Include(produto => produto.produto).ToListAsync());

        }
        // GET: Ordem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordem = await _context.Ordem
                .Include(cliente => cliente.cliente)
                .Include(produto => produto.produto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordem == null)
            {
                return NotFound();
            }

            return View(ordem);
        }

        // GET: Ordem/Create
        public IActionResult Create()
        {
            ViewData["Cliente"] = new SelectList(_context.Cliente.ToList(), "Cliente").Items;
            ViewData["Produto"] = new SelectList(_context.Produto.ToList(), "Produto").Items;
            return View();
        }

        // POST: Ordem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,data,hora,qdte")] Ordem ordem, IFormCollection collection)
        {
            int idProduto = Convert.ToInt16(collection["Produto"]);
            int idCliente = Convert.ToInt16(collection["Cliente"]);
            if (ModelState.IsValid)
            {
                var prod = await _context.Produto.FindAsync(idProduto);
                ordem.produto = prod;
                var cli = await _context.Cliente.FindAsync(idCliente);
                ordem.cliente = cli;
               

                _context.Add(ordem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordem);
        }

        // GET: Ordem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordem = await _context.Ordem.FindAsync(id);
            if (ordem == null)
            {
                return NotFound();
            }
            ViewData["Cliente"] = new SelectList(_context.Cliente.ToList(), "Cliente").Items;
            ViewData["Produto"] = new SelectList(_context.Produto.ToList(), "Produto").Items;
            return View(ordem);
        }

        // POST: Ordem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,data,hora,qdte")] Ordem ordem, IFormCollection collection)
        {
            if (id != ordem.ID)
            {
                return NotFound();
            }
            int idProduto = Convert.ToInt16(collection["Produto"]);
            int idCliente = Convert.ToInt16(collection["Cliente"]);
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var prod = await _context.Produto.FindAsync(idProduto);
                    ordem.produto = prod;
                    var cli = await _context.Cliente.FindAsync(idCliente);
                    ordem.cliente = cli;


                    _context.Update(ordem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdemExists(ordem.ID))
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
            return View(ordem);
        }

        // GET: Ordem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordem = await _context.Ordem
                .Include(cliente => cliente.cliente)
                .Include(produto => produto.produto)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ordem == null)
            {
                return NotFound();
            }

            return View(ordem);
        }

        // POST: Ordem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordem = await _context.Ordem.FindAsync(id);
            _context.Ordem.Remove(ordem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdemExists(int id)
        {
            return _context.Ordem.Any(e => e.ID == id);
        }
    }
}
