using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;

namespace Biblioteca.Controllers
{
    public class CreateBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CreateBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreateBook
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: CreateBook/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createBookModel = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (createBookModel == null)
            {
                return NotFound();
            }

            return View(createBookModel);
        }

        // GET: CreateBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreateBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,NomeLivro,Quantitie")] CreateBookModel createBookModel)
        {
            bool book = await _context.Books.AnyAsync(b => b.NomeLivro.ToLower() == createBookModel.NomeLivro.ToLower());

            if(book)
            {
                ModelState.AddModelError("NomeLivro", "Livro já cadastrado.");
                return View(createBookModel);
            }


            if (ModelState.IsValid)
            {
                _context.Add(createBookModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createBookModel);
        }

        // GET: CreateBook/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createBookModel = await _context.Books.FindAsync(id);
            if (createBookModel == null)
            {
                return NotFound();
            }
            return View(createBookModel);
        }

        // POST: CreateBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,NomeLivro,Quantitie")] CreateBookModel createBookModel)
        {
            if (id != createBookModel.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(createBookModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreateBookModelExists(createBookModel.BookId))
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
            return View(createBookModel);
        }

        // GET: CreateBook/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createBookModel = await _context.Books
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (createBookModel == null)
            {
                return NotFound();
            }

            return View(createBookModel);
        }

        // POST: CreateBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var createBookModel = await _context.Books.FindAsync(id);
            if (createBookModel != null)
            {
                _context.Books.Remove(createBookModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreateBookModelExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
