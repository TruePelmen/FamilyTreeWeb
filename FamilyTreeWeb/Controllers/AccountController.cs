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

        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Manage()
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
                return RedirectToAction("IndexAuth", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your username and password.");
                return View(user);
            }
        }


        [HttpDelete]
        public IActionResult DeleteAccount(string login)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Login == login);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult EditAccount(string login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult EditAccount(User editedUser)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = _context.Users.FirstOrDefault(u => u.Login == editedUser.Login);
                if (userToUpdate != null)
                {
                    // Оновлюємо дані користувача зміненими даними
                    userToUpdate.Login = editedUser.Login;
                    userToUpdate.Password = editedUser.Password;
                    // Зберігаємо зміни в базі даних
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                // Якщо дані не коректні, повертаємо користувача на сторінку редагування з повідомленнями про помилки
                return View(editedUser);
            }
        }

    }
}