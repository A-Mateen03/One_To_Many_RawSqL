using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using One_To_Many_RawSqL.Data;
using One_To_Many_RawSqL.Models;

namespace One_To_Many_RawSqL.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {

            var products = await _context.Products.FromSqlRaw("SELECT * FROM Products").ToListAsync();
            return View(products);
              }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FromSqlInterpolated($"SELECT * FROM Products WHERE P_ID = {id}").FirstOrDefaultAsync();
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("P_ID,P_Name,P_Price,P_Detail,P_ImgUrl")] Products products)
        {
            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Products (P_Name,P_Price,P_Detail,P_ImgUrl) VALUES ({products.P_Name},{products.P_Price},{products.P_Detail},{products.P_ImgUrl})");

                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FromSqlInterpolated($"SELECT * FROM Products WHERE P_ID={id}").FirstOrDefaultAsync();
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("P_ID,P_Name,P_Price,P_Detail,P_ImgUrl")] Products products)
        {
            if (id != products.P_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Products SET P_Name = {products.P_Name},P_Price={products.P_Price},P_Detail={products.P_Detail},P_ImgUrl={products.P_ImgUrl} WHERE P_ID = {id}");  
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FromSqlInterpolated($"SELECT * FROM Products WHERE P_ID = {id}").FirstOrDefaultAsync();
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM Products WHERE P_ID={id}");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            var product = _context.Products.FromSqlInterpolated($"SELECT * FROM Products WHERE P_ID = {id}").FirstOrDefault();
            return product != null;
        }
    }
}
