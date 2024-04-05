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
    public class EmpleadosController : Controller
    {
        private readonly ComprasIso810Context _context;
        private readonly ValidacionCedula _validacionCedula; // NEW

        public EmpleadosController(ComprasIso810Context context)
        {
            _context = context;
            _validacionCedula = new ValidacionCedula(); // NEW
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var comprasIso810Context = _context.Empleados.Include(e => e.DepartamentoNavigation);
            return View(await comprasIso810Context.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.DepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "Nombre");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,Departamento,Estado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                // NEW: Validación de la cédula
                ValidacionCedula validacionCedula = new ValidacionCedula(); // NEW
                if (!validacionCedula.ValidateCedula(empleado.Cedula)) // NEW
                {
                    ModelState.AddModelError("Cedula", "Cédula incorrecta o duplicada."); // NEW
                    ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.Departamento); // NEW
                    return View(empleado); // NEW
                }
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "Nombre", empleado.Departamento);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,Departamento,Estado")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // NEW: Validación de la cédula
                if (!_validacionCedula.ValidateCedula(empleado.Cedula)) // NEW
                {
                    ModelState.AddModelError("Cedula", "Cédula incorrecta o duplicada."); // NEW
                    ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.Departamento); // NEW
                    return View(empleado); // NEW
                }
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
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
            ViewData["Departamento"] = new SelectList(_context.Departamentos, "Id", "Id", empleado.Departamento);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .Include(e => e.DepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleados.Any(e => e.Id == id);
        }
    }
}
