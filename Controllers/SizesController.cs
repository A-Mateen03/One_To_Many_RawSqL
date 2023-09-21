using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using One_To_Many_RawSqL.Data;
using One_To_Many_RawSqL.Models;

namespace One_To_Many_RawSqL.Controllers
{
    public class SizesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SizesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sizes
        public async Task<IActionResult> Index()
        {
          
                // Use raw SQL query to fetch sizes with associated products
                var sizes = await _context.Sizes.FromSqlRaw("SELECT * FROM Sizes").Include(s => s.Products).ToListAsync();
                return View(sizes);
            
           
            
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
                // Use raw SQL query with parameter binding to fetch a specific size with associated products
                var size =await _context.Sizes.FromSqlInterpolated($"SELECT * FROM Sizes WHERE SizeId = {id}")
                    .Include(s => s.Products)
                    .FirstOrDefaultAsync();

                if (size == null)
                {
                    return NotFound();
                }

                return View(size);
            
        }

        // GET: Sizes/Create
        public IActionResult Create()
        {
            ViewData["P_ID"] = new SelectList(_context.Products, "P_ID", "P_ID");
            return View();
        }

        // POST: Sizes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SizeId,Size,P_ID")] Sizes sizes)
        {
            if (ModelState.IsValid)
            {

                // Use raw SQL query to insert a new size
                await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Sizes (Size, P_ID) VALUES ({sizes.Size}, {sizes.P_ID})");
                    return RedirectToAction(nameof(Index));
               
            }
            ViewData["P_ID"] = new SelectList(_context.Products, "P_ID", "P_ID", sizes.P_ID);
            return View(sizes);
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
                // Use raw SQL query with parameter binding to fetch a specific size for editing
                var size =await _context.Sizes.FromSqlInterpolated($"SELECT * FROM Sizes WHERE SizeId = {id}")
                    .Include(s => s.Products)
                    .FirstOrDefaultAsync();

                if (size == null)
                {
                    return NotFound();
                }

                ViewData["P_ID"] = new SelectList(_context.Products, "P_ID", "P_ID", size.P_ID);
                return View(size);
            
            
        }

        // POST: Sizes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SizeId,Size,P_ID")] Sizes sizes)
        {
            if (id != sizes.SizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    // Use raw SQL query with parameter binding to update the size
                   await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Sizes SET Size = {sizes.Size}, P_ID = {sizes.P_ID} WHERE SizeId = {id}");
                
               

                return RedirectToAction(nameof(Index));
            }
            ViewData["P_ID"] = new SelectList(_context.Products, "P_ID", "P_ID", sizes.P_ID);
            return View(sizes);
        }

        // GET: Sizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
                // Use raw SQL query with parameter binding to fetch a specific size for deletion
                var size =await _context.Sizes.FromSqlInterpolated($"SELECT * FROM Sizes WHERE SizeId = {id}")
                    .Include(s => s.Products)
                    .FirstOrDefaultAsync();

                if (size == null)
                {
                    return NotFound();
                }

                return View(size);

        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            // Use raw SQL query with parameter binding to delete the size
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Sizes WHERE SizeId = {id}");
                return RedirectToAction(nameof(Index));
            
            
        }

        private bool SizesExists(int id)
        {

            var exist = _context.Sizes
             .FromSqlInterpolated($"SELECT * FROM Sizes WHERE SizeId = {id}")
             .FirstOrDefault();

            return exist != null;

        }
    }
}
