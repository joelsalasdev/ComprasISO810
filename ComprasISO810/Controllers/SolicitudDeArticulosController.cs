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
    public class SolicitudDeArticulosController : Controller
    {
        private readonly ComprasIso810Context _context;

        public SolicitudDeArticulosController(ComprasIso810Context context)
        {
            _context = context;
        }

        // GET: SolicitudDeArticulos
        public async Task<IActionResult> Index()
        {
            var comprasIso810Context = _context.SolicitudDeArticulos.Include(s => s.ArticuloNavigation).Include(s => s.EmpleadoSolicitanteNavigation).Include(s => s.UnidadesDeMedidaNavigation);
            return View(await comprasIso810Context.ToListAsync());
        }

        // GET: SolicitudDeArticulos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudDeArticulo = await _context.SolicitudDeArticulos
                .Include(s => s.ArticuloNavigation)
                .Include(s => s.EmpleadoSolicitanteNavigation)
                .Include(s => s.UnidadesDeMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudDeArticulo == null)
            {
                return NotFound();
            }

            return View(solicitudDeArticulo);
        }

        // GET: SolicitudDeArticulos/Create
        public IActionResult Create()
        {
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Descripcion");
            ViewData["EmpleadoSolicitante"] = new SelectList(_context.Empleados, "Id", "Nombre");
            ViewData["UnidadesDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion");
            return View();
        }

        // POST: SolicitudDeArticulos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmpleadoSolicitante,FechaSolicitud,Articulo,Cantidad,UnidadesDeMedida,Estado")] SolicitudDeArticulo solicitudDeArticulo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitudDeArticulo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Id", solicitudDeArticulo.Articulo);
            ViewData["EmpleadoSolicitante"] = new SelectList(_context.Empleados, "Id", "Id", solicitudDeArticulo.EmpleadoSolicitante);
            ViewData["UnidadesDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", solicitudDeArticulo.UnidadesDeMedida);
            return View(solicitudDeArticulo);
        }

        // GET: SolicitudDeArticulos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudDeArticulo = await _context.SolicitudDeArticulos.FindAsync(id);
            if (solicitudDeArticulo == null)
            {
                return NotFound();
            }
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Descripcion", solicitudDeArticulo.Articulo);
            ViewData["EmpleadoSolicitante"] = new SelectList(_context.Empleados, "Id", "Nombre", solicitudDeArticulo.EmpleadoSolicitante);
            ViewData["UnidadesDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Descripcion", solicitudDeArticulo.UnidadesDeMedida);
            return View(solicitudDeArticulo);
        }

        // POST: SolicitudDeArticulos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmpleadoSolicitante,FechaSolicitud,Articulo,Cantidad,UnidadesDeMedida,Estado")] SolicitudDeArticulo solicitudDeArticulo)
        {
            if (id != solicitudDeArticulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudDeArticulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudDeArticuloExists(solicitudDeArticulo.Id))
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
            ViewData["Articulo"] = new SelectList(_context.Articulos, "Id", "Id", solicitudDeArticulo.Articulo);
            ViewData["EmpleadoSolicitante"] = new SelectList(_context.Empleados, "Id", "Id", solicitudDeArticulo.EmpleadoSolicitante);
            ViewData["UnidadesDeMedida"] = new SelectList(_context.UnidadesDeMedida, "Id", "Id", solicitudDeArticulo.UnidadesDeMedida);
            return View(solicitudDeArticulo);
        }

        // GET: SolicitudDeArticulos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudDeArticulo = await _context.SolicitudDeArticulos
                .Include(s => s.ArticuloNavigation)
                .Include(s => s.EmpleadoSolicitanteNavigation)
                .Include(s => s.UnidadesDeMedidaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solicitudDeArticulo == null)
            {
                return NotFound();
            }

            return View(solicitudDeArticulo);
        }

        // POST: SolicitudDeArticulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudDeArticulo = await _context.SolicitudDeArticulos.FindAsync(id);
            if (solicitudDeArticulo != null)
            {
                _context.SolicitudDeArticulos.Remove(solicitudDeArticulo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudDeArticuloExists(int id)
        {
            return _context.SolicitudDeArticulos.Any(e => e.Id == id);
        }
    }
}
