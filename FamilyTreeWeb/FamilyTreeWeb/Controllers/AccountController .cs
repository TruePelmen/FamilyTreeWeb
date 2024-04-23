using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authorization;

namespace FamilyTreeWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly FamilyTreeDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, FamilyTreeDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

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
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userTemp = new User { Login = user.Login};
                var result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(user);
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


        [HttpPost]
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