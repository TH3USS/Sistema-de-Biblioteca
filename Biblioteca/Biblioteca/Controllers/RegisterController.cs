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
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Registers.Include(r => r.Book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Register/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerModel = await _context.Registers
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.RigisterId == id);
            if (registerModel == null)
            {
                return NotFound();
            }

            return View(registerModel);
        }

        // GET: Register/Create
        public IActionResult Create()
        {
            var registerModel = new RegisterModel
            {
                Emprestimo = DateTime.Now // Define a data atual ao abrir o formulário
            };
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "NomeLivro");
            return View(registerModel);
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RigisterId,BookId,Returned,Emprestimo,Devolucao")] RegisterModel registerModel)
        {
            registerModel.Emprestimo = DateTime.Now;

            var book = _context.Books.Find(registerModel.BookId);


            if (book == null)
            {
                return NotFound("Livro não encontrado.");
            }

            int registersActive = _context.Registers.Count(e => e.BookId == registerModel.BookId && !e.Returned);

            if (registersActive >= book.Quantitie)
            {
                ModelState.AddModelError("", "Não há exemplares disponíveis para empréstimo.");
                ViewData["BookId"] = new SelectList(_context.Books, "BookId", "NomeLivro", registerModel.BookId);
                return View(registerModel);
            }

            if (ModelState.IsValid)
            {
                _context.Add(registerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "NomeLivro", registerModel.BookId);
            return View(registerModel);
        }

        // GET: Register/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerModel = await _context.Registers.FindAsync(id);
            if (registerModel == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "NomeLivro", registerModel.BookId);
            return View(registerModel);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RigisterId,BookId,Returned,Emprestimo,Devolucao")] RegisterModel registerModel)
        {
            if (id != registerModel.RigisterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegisterModelExists(registerModel.RigisterId))
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
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "NomeLivro", registerModel.BookId);
            return View(registerModel);
        }

        // GET: Register/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registerModel = await _context.Registers
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.RigisterId == id);
            if (registerModel == null)
            {
                return NotFound();
            }

            return View(registerModel);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registerModel = await _context.Registers.FindAsync(id);
            if (registerModel != null)
            {
                _context.Registers.Remove(registerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegisterModelExists(int id)
        {
            return _context.Registers.Any(e => e.RigisterId == id);
        }
    }
}
