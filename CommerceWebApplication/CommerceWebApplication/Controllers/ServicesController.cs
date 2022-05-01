using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CommerceWebApplication.Models;

namespace CommerceWebApplication.Controllers
{
    public class ServicesController : Controller
    {
        private readonly CommerceContext _context;

        public ServicesController(CommerceContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var commerceContext = _context.Operators.Include(c => c.City).Include(c => c.LicenseType);
            return View(await commerceContext.ToListAsync());
        }

      
        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            ViewData["LicenseTypeId"] = new SelectList(_context.LicenseTypes, "Id", "LicenseTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Desc,LicenseId,Created,Expired,LicenseTypeId,CityId")] Operator coperator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coperator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            ViewData["LicenseTypeId"] = new SelectList(_context.LicenseTypes, "Id", "LicenseTypeName");
            return View(coperator);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coperator = await _context.Operators.FindAsync(id);
            if (coperator == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            ViewData["LicenseTypeId"] = new SelectList(_context.LicenseTypes, "Id", "LicenseTypeName");
            return View(coperator);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Desc,LicenseId,Created,Expired,LicenseTypeId,CityId")] Operator coperator)
        {
            if (id != coperator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coperator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperatorExists(coperator.Id))
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
            ViewData["StateId"] = new SelectList(_context.States, "Id", "StateName");
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "CityName");
            ViewData["LicenseTypeId"] = new SelectList(_context.LicenseTypes, "Id", "LicenseTypeName");
            return View(coperator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coperator = await _context.Operators.FindAsync(id);
            _context.Operators.Remove(coperator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatorExists(int id)
        {
            return _context.Operators.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetCites(int id)
        {
            if (id > 0)
            {
                var list = await _context.Cities.Where(c => c.StateId == id).ToListAsync();

                return Json(list);
            }
            return null;
        }

    }
}
