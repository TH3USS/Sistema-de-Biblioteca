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
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CreateUser/Create
        public IActionResult CreateUser()
        {
            return View();
        }

        // POST: CreateUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("UserId,UserName,Password,PasswordConfirm")] LoginModel createUserModel)
        {
            bool newUser = await _context.Users.AnyAsync(u => u.UserName == createUserModel.UserName);

            if (newUser)
            {
                ModelState.AddModelError("UserName", "Usuário com este nome já existe.");
                return View(createUserModel);
            }

            if(createUserModel.Password.Length < 8)
            {
                ModelState.AddModelError("Password", "Senha muito pequena.");
                return View(createUserModel);
            }


            if (createUserModel.Password.ToString() != createUserModel.PasswordConfirm.ToString())
            {
                ModelState.AddModelError("PasswordConfirm", "Senha incorreta.");
                return View(createUserModel);
            }

            if (ModelState.IsValid)
            {                
                _context.Add(createUserModel);
                await _context.SaveChangesAsync();
                return View("~/Views/Home/Index.cshtml");
            }
            return View(createUserModel);
        }

        // GET: CreateUser/Edit/5
        public IActionResult Login()
        {
            return View();
        }

        // POST: CreateUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserId,UserName,Password")] LoginModel createUserModel)
        {
            bool users = await _context.Users.AnyAsync(u => u.UserName == createUserModel.UserName);
                bool passwords = await _context.Users.AnyAsync(u => u.Password == createUserModel.Password);

                if (users && passwords)
                {
                    return View("~/Views/Home/Index.cshtml");
                }
            return View(createUserModel);
        }
    }
}
