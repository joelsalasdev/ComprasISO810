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
    public class UnidadesDeMedidumController : Controller
    {
        private readonly ComprasIso810Context _context;

        public UnidadesDeMedidumController(ComprasIso810Context context)
        {
            _context = context;
        }

        // GET: UnidadesDeMedidum
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadesDeMedida.ToListAsync());
        }

        // GET: UnidadesDeMedidum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadesDeMedidum = await _context.UnidadesDeMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadesDeMedidum == null)
            {
                return NotFound();
            }

            return View(unidadesDeMedidum);
        }

        // GET: UnidadesDeMedidum/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadesDeMedidum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Estado")] UnidadesDeMedidum unidadesDeMedidum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadesDeMedidum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadesDeMedidum);
        }

        // GET: UnidadesDeMedidum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadesDeMedidum = await _context.UnidadesDeMedida.FindAsync(id);
            if (unidadesDeMedidum == null)
            {
                return NotFound();
            }
            return View(unidadesDeMedidum);
        }

        // POST: UnidadesDeMedidum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Estado")] UnidadesDeMedidum unidadesDeMedidum)
        {
            if (id != unidadesDeMedidum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadesDeMedidum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadesDeMedidumExists(unidadesDeMedidum.Id))
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
            return View(unidadesDeMedidum);
        }

        // GET: UnidadesDeMedidum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadesDeMedidum = await _context.UnidadesDeMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadesDeMedidum == null)
            {
                return NotFound();
            }

            return View(unidadesDeMedidum);
        }

        // POST: UnidadesDeMedidum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadesDeMedidum = await _context.UnidadesDeMedida.FindAsync(id);
            if (unidadesDeMedidum != null)
            {
                _context.UnidadesDeMedida.Remove(unidadesDeMedidum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadesDeMedidumExists(int id)
        {
            return _context.UnidadesDeMedida.Any(e => e.Id == id);
        }
    }
}
