using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FamilyTreeWeb.Controllers
{
    public class ProfileController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public ProfileController(FamilyTreeDbContext context)
        {
            _context = context;
        }

        [Route("Profile/{id}")]
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _context.People
                         .Include(p => p.Events)
                         .Include(p => p.Photos)
                         .Include(p => p.RelationshipPersonId1Navigations)
                             .ThenInclude(r => r.PersonId2Navigation)
                         .Include(p => p.RelationshipPersonId2Navigations)
                             .ThenInclude(r => r.PersonId1Navigation)
                         .FirstOrDefault(p => p.Id == id);

            person.Children = new List<Person>();
            foreach (var relationship in person.RelationshipPersonId1Navigations)
            {
                if (relationship.RelationshipType == "father-child" || relationship.RelationshipType == "mother-child")
                {
                    var child = relationship.PersonId2Navigation;
                    person.Children.Add(child);
                }
            }

            // Вибрати батька та матір особи
            person.Father = person.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "father-child")?.PersonId1Navigation;
            person.Mother = person.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "mother-child")?.PersonId1Navigation;
            if (person.Gender == "male")
            {
                person.Spouse = person.RelationshipPersonId1Navigations.FirstOrDefault(r => r.RelationshipType == "spouse")?.PersonId2Navigation;
            }
            else
            {
                person.Spouse = person.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "spouse")?.PersonId1Navigation;
            }

            person.Events = person.Events.OrderBy(e => e.EventDate).ToList();
            
            

            //// Вибрати всіх дітей батька та матері
            //var fatherChildren = person.Father?.RelationshipPersonId1Navigations
            //    .Where(r => (r.RelationshipType == "father-child" || r.RelationshipType == "mother-child") && r.PersonId2Navigation.Id != person.Id)
            //    .Select(r => r.PersonId2Navigation)
            //    .ToList();

            //var motherChildren = person.Mother?.RelationshipPersonId1Navigations
            //    .Where(r => (r.RelationshipType == "father-child" || r.RelationshipType == "mother-child") && r.PersonId2Navigation.Id != person.Id)
            //    .Select(r => r.PersonId2Navigation)
            //    .ToList();

            //// Об'єднати всіх дітей батька та матері
            //var allChildren = (fatherChildren ?? Enumerable.Empty<Person>()).Union(motherChildren ?? Enumerable.Empty<Person>());

            //// Вибрати братів та сестер
            //person.Siblings = allChildren.Where(c => c.Id != person.Id).ToList();

            //if (person == null)
            //{
            //    return NotFound();
            //}

            return View(person);
        }

        [HttpGet]
        public IActionResult CreateEventForm(int personId)
        {
            var model = new Event();
            model.PersonId = personId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event model)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile", new { id = model.PersonId });
            }
            return View(model);
        }
    }
}

