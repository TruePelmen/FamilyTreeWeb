using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public AccountController(FamilyTreeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(newUser);
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
            if (existingUser != null)
            {
                // Логіка для успішного входу, наприклад, збереження інформації про користувача в сеансі (Session)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username and password.");
                return View(user);
            }
        }
    }
}