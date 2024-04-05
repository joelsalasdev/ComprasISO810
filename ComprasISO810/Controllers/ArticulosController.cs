using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComprasISO810.Models;

namespace ComprasISO810.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly ComprasIso810Context _context;

        public ArticulosController(ComprasIso810Context context)
        {
            _context = context;
        }

        // GET: Articulos
        public async Task<IActionResult> Index()
        {
            var comprasIso810Context = _context.Articulos.Include(a => a.MarcaNavigation).Include(a => a.UnidadDeMedidaNavigation);
            return View(await comprasIso810Context.ToListAsync());
        }

        // GET: Articulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.MarcaNavigation)
                .Include(a => a.UnidadDeMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // GET: Articulos/Create
        public IActionResult Create()
        {
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Descripcion");
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion");
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Marca,UnidadDeMedida,Existencia,Estado")] Articulo articulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(articulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Id", articulo.Marca);
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", articulo.UnidadDeMedida);
            return View(articulo);
        }

        // GET: Articulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Descripcion", articulo.Marca);
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion", articulo.UnidadDeMedida);
            return View(articulo);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Marca,UnidadDeMedida,Existencia,Estado")] Articulo articulo)
        {
            if (id != articulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(articulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticuloExists(articulo.Id))
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
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Id", articulo.Marca);
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", articulo.UnidadDeMedida);
            return View(articulo);
        }

        // GET: Articulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var articulo = await _context.Articulos
                .Include(a => a.MarcaNavigation)
                .Include(a => a.UnidadDeMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (articulo == null)
            {
                return NotFound();
            }

            return View(articulo);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo != null)
            {
                _context.Articulos.Remove(articulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.Id == id);
        }
    }
}
