using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamsHotRods.Data;
using SamsHotRods.Models;

namespace SamsHotRods.Controllers
{
    [Authorize(Roles = "Admin, PaidCust")]
    public class UserOnliesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserOnliesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: UserOnlies
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserOnly.ToListAsync());
        }

        // GET: UserOnlies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOnly = await _context.UserOnly
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userOnly == null)
            {
                return NotFound();
            }

            return View(userOnly);
        }
        
        // GET: UserOnlies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserOnlies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Priority,UserType,Story,SoThat")] UserOnly userOnly)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userOnly);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userOnly);
        }

        // GET: UserOnlies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOnly = await _context.UserOnly.SingleOrDefaultAsync(m => m.Id == id);
            if (userOnly == null)
            {
                return NotFound();
            }
            return View(userOnly);
        }

        // POST: UserOnlies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Priority,UserType,Story,SoThat")] UserOnly userOnly)
        {
            if (id != userOnly.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userOnly);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserOnlyExists(userOnly.Id))
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
            return View(userOnly);
        }
        
        // GET: UserOnlies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOnly = await _context.UserOnly
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userOnly == null)
            {
                return NotFound();
            }

            return View(userOnly);
        }

        // POST: UserOnlies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userOnly = await _context.UserOnly.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserOnly.Remove(userOnly);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserOnlyExists(int id)
        {
            return _context.UserOnly.Any(e => e.Id == id);
        }
    }
}
