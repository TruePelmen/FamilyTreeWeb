using FamilyTreeWeb.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyTreeWeb.Models;

namespace FamilyTreeWeb.Controllers
{
    public class PhotoController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public PhotoController(FamilyTreeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Photo(int id)
        {
            var photo = _context.Photos
                .Include(p => p.Person)
                .ThenInclude(p => p.Tree)
                .FirstOrDefault(p => p.Id == id);

            var person = _context.People
                .Include(p => p.Photos)
                .FirstOrDefault(p => p.Id == photo.PersonId);

            photo.Person = person;

            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        public async Task<IActionResult> Gallery(int id)
        {
            var tree = _context.Trees
                .Include(t => t.People)
                .ThenInclude(p => p.Photos)
                .FirstOrDefault(t => t.Id == id);

            List<Photo> photos = new List<Photo>();
            var people = tree.People.ToList();

            foreach (var person in people)
            {
                photos.AddRange(person.Photos);
            }

            return View(photos);
        }
    }
}
