using FamilyTreeWeb.Context;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeWeb.Controllers
{
    public class PhotoController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public PhotoController(FamilyTreeDbContext context)
        {
            _context = context;
        }

        [Route("Photo/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }
    }
}
