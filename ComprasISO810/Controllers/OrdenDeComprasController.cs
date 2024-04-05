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
    public class OrdenDeComprasController : Controller
    {
        private readonly ComprasIso810Context _context;

        public OrdenDeComprasController(ComprasIso810Context context)
        {
            _context = context;
        }

        // GET: OrdenDeCompras
        public async Task<IActionResult> Index()
        {
            var comprasIso810Context = _context.OrdenDeCompras.Include(o => o.ArticuloNavigation).Include(o => o.IdSolicitudNavigation).Include(o => o.MarcaNavigation).Include(o => o.UnidadDeMedidaNavigation);
            return View(await comprasIso810Context.ToListAsync());
        }

        // GET: OrdenDeCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDeCompra = await _context.OrdenDeCompras
                .Include(o => o.ArticuloNavigation)
                .Include(o => o.IdSolicitudNavigation)
                .Include(o => o.MarcaNavigation)
                .Include(o => o.UnidadDeMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDeCompra == null)
            {
                return NotFound();
            }

            return View(ordenDeCompra);
        }

        // GET: OrdenDeCompras/Create
        public IActionResult Create()
        {
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Descripcion");
            ViewData["IdSolicitud"] = new SelectList(_context.SolicitudDeArticulos, "Id", "Id");
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Descripcion");
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion");
            return View();
        }

        // POST: OrdenDeCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdSolicitud,FechaOrden,Estado,Articulo,Cantidad,UnidadDeMedida,Marca")] OrdenDeCompra ordenDeCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenDeCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Id", ordenDeCompra.Articulo);
            ViewData["IdSolicitud"] = new SelectList(_context.SolicitudDeArticulos, "Id", "Id", ordenDeCompra.IdSolicitud);
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Id", ordenDeCompra.Marca);
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", ordenDeCompra.UnidadDeMedida);
            return View(ordenDeCompra);
        }

        // GET: OrdenDeCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDeCompra = await _context.OrdenDeCompras.FindAsync(id);
            if (ordenDeCompra == null)
            {
                return NotFound();
            }
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Descripcion", ordenDeCompra.Articulo);
            ViewData["IdSolicitud"] = new SelectList(_context.SolicitudDeArticulos, "Id", "Id", ordenDeCompra.IdSolicitud);
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Descripcion", ordenDeCompra.Marca);
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion", ordenDeCompra.UnidadDeMedida);
            return View(ordenDeCompra);
        }

        // POST: OrdenDeCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdSolicitud,FechaOrden,Estado,Articulo,Cantidad,UnidadDeMedida,Marca")] OrdenDeCompra ordenDeCompra)
        {
            if (id != ordenDeCompra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenDeCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenDeCompraExists(ordenDeCompra.Id))
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
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Id", ordenDeCompra.Articulo);
            ViewData["IdSolicitud"] = new SelectList(_context.SolicitudDeArticulos, "Id", "Id", ordenDeCompra.IdSolicitud);
            ViewData["Marca"] = new SelectList(_context.Marcas, "Id", "Id", ordenDeCompra.Marca);
            ViewData["UnidadDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", ordenDeCompra.UnidadDeMedida);
            return View(ordenDeCompra);
        }

        // GET: OrdenDeCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenDeCompra = await _context.OrdenDeCompras
                .Include(o => o.ArticuloNavigation)
                .Include(o => o.IdSolicitudNavigation)
                .Include(o => o.MarcaNavigation)
                .Include(o => o.UnidadDeMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordenDeCompra == null)
            {
                return NotFound();
            }

            return View(ordenDeCompra);
        }

        // POST: OrdenDeCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenDeCompra = await _context.OrdenDeCompras.FindAsync(id);
            if (ordenDeCompra != null)
            {
                _context.OrdenDeCompras.Remove(ordenDeCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenDeCompraExists(int id)
        {
            return _context.OrdenDeCompras.Any(e => e.Id == id);
        }
    }
}
