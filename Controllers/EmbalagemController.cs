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
    public class EmbalagemController : Controller
    {
        private readonly ProjFinalContext _context;

        public EmbalagemController(ProjFinalContext context)
        {
            _context = context;
        }

        // GET: Embalagem
        public async Task<IActionResult> Index()
        {
                       return View(await _context.Embalagem
            .Include(fornecedor => fornecedor.fornecedor)
            .Include(produto => produto.produto).ToListAsync());
        }

        // GET: Embalagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var embalagem = await _context.Embalagem
                .Include(produto => produto.produto)
                .Include(fornecedor => fornecedor.fornecedor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (embalagem == null)
            {
                return NotFound();
            }

            return View(embalagem);
        }

        // GET: Embalagem/Create
        public IActionResult Create()
        {
            ViewData["Produto"] = new SelectList(_context.Produto.ToList(), "Produto").Items;
            ViewData["Fornecedor"] = new SelectList(_context.Fornecedor.ToList(), "Fornecedor").Items;
            return View();
        }

        // POST: Embalagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,nome,tamanho,qtde")] Embalagem embalagem, IFormCollection collection)
        {
             int idProduto = Convert.ToInt16(collection["Produto"]);
             int idFornecedor = Convert.ToInt16(collection["Fornecedor"]);
                 
            if (ModelState.IsValid)
            {
                var prod = await _context.Produto.FindAsync(idProduto);
                embalagem.produto = prod;

                var forn = await _context.Fornecedor.FindAsync(idFornecedor);
                embalagem.fornecedor = forn;
            

                _context.Add(embalagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(embalagem);
        }

        // GET: Embalagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var embalagem = await _context.Embalagem.FindAsync(id);
            if (embalagem == null)
            {
                return NotFound();
            }

            ViewData["Produto"] = new SelectList(_context.Produto.ToList(), "Produto").Items;
            ViewData["Fornecedor"] = new SelectList(_context.Fornecedor.ToList(), "Fornecedor").Items;
            
            return View(embalagem);
        }

        // POST: Embalagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,nome,tamanho,qtde")] Embalagem embalagem, IFormCollection collection)
        {
            if (id != embalagem.ID)
            {
                return NotFound();
            }
            int idProduto = Convert.ToInt16(collection["Produto"]);
            int idFornecedor = Convert.ToInt16(collection["Fornecedor"]);
             
            if (ModelState.IsValid)
            {
                try
                {
                    var prod = await _context.Produto.FindAsync(idProduto);
                    embalagem.produto = prod;
                    var forn = await _context.Fornecedor.FindAsync(idFornecedor);
                    embalagem.fornecedor = forn;

                    _context.Update(embalagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmbalagemExists(embalagem.ID))
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
            return View(embalagem);
        }

        // GET: Embalagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var embalagem = await _context.Embalagem
                .Include(produto => produto.produto)
                .Include(fornecedor => fornecedor.fornecedor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (embalagem == null)
            {
                return NotFound();
            }

            return View(embalagem);
        }

        // POST: Embalagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var embalagem = await _context.Embalagem.FindAsync(id);
            _context.Embalagem.Remove(embalagem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmbalagemExists(int id)
        {
            return _context.Embalagem.Any(e => e.ID == id);
        }
    }
}
