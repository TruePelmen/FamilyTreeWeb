using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeWeb.domain.Controllers
{
    public class Tree : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
