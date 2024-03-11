using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNew.Data;
using WebNew.Models;

namespace WebNew.Controllers
{
    public class RollesAdminController : Controller
    {
        private readonly WebNewsContext _context;

        public RollesAdminController(WebNewsContext context)
        {
            _context = context;
        }

        // GET: RollesAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rolles.ToListAsync());
        }

        // GET: RollesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolles = await _context.Rolles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolles == null)
            {
                return NotFound();
            }

            return View(rolles);
        }

        // GET: RollesAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RollesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameRolles")] Rolles rolles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rolles);
        }

        // GET: RollesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolles = await _context.Rolles.FindAsync(id);
            if (rolles == null)
            {
                return NotFound();
            }
            return View(rolles);
        }

        // POST: RollesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameRolles")] Rolles rolles)
        {
            if (id != rolles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RollesExists(rolles.Id))
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
            return View(rolles);
        }

        // GET: RollesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolles = await _context.Rolles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolles == null)
            {
                return NotFound();
            }

            return View(rolles);
        }

        // POST: RollesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rolles = await _context.Rolles.FindAsync(id);
            if (rolles != null)
            {
                _context.Rolles.Remove(rolles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RollesExists(int id)
        {
            return _context.Rolles.Any(e => e.Id == id);
        }
    }
}
