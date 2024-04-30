using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeWeb.Controllers
{
    public class TreeController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public TreeController(FamilyTreeDbContext context)
        {
            _context = context;
        }

        [Route("Tree/{id}")]
        public async Task<IActionResult> ViewTree(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _context.People
                         .Include(p => p.Photos)
                         .Include(p => p.Tree)
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
                    var child = _context.People
                        .Include(p => p.Photos)
                        .Include(p => p.RelationshipPersonId1Navigations)
                            .ThenInclude(r => r.PersonId2Navigation)
                        .Include(p => p.RelationshipPersonId2Navigations)
                            .ThenInclude(r => r.PersonId1Navigation)
                        .FirstOrDefault(p => p.Id == relationship.PersonId2);
                    person.Children.Add(child);
                }
            }

            // Вибрати батька та матір особи
            person.Father = person.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "father-child")?.PersonId1Navigation;
            if (person.Father != null)
            {
                person.Father = _context.People
                    .Include(p => p.Photos)
                    .Include(p => p.RelationshipPersonId1Navigations)
                    .ThenInclude(p => p.PersonId2Navigation)
                    .Include(p => p.RelationshipPersonId2Navigations)
                    .ThenInclude(p => p.PersonId1Navigation)
                    .FirstOrDefault(p => p.Id == person.Father.Id);
            }
            person.Mother = person.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "mother-child")?.PersonId1Navigation;
            if (person.Mother != null)
            {
                person.Mother = _context.People
                    .Include(p => p.Photos)
                    .Include(p => p.RelationshipPersonId1Navigations)
                    .ThenInclude(p => p.PersonId2Navigation)
                    .Include(p => p.RelationshipPersonId2Navigations)
                    .ThenInclude(p => p.PersonId1Navigation)
                    .FirstOrDefault(p => p.Id == person.Mother.Id);
            }

            if (person.Gender == "male")
            {
                person.Spouse = person.RelationshipPersonId1Navigations.FirstOrDefault(r => r.RelationshipType == "spouse")?.PersonId2Navigation;
            }
            else
            {
                person.Spouse = person.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "spouse")?.PersonId1Navigation;
            }

            if (person.Spouse != null)
            {
                person.Spouse = _context.People
                   .Include(p => p.Photos)
                   .Include(p => p.RelationshipPersonId1Navigations)
                   .ThenInclude(p => p.PersonId2Navigation)
                   .Include(p => p.RelationshipPersonId2Navigations)
                   .ThenInclude(p => p.PersonId1Navigation)
                   .FirstOrDefault(p => p.Id == person.Spouse.Id);
                person.Spouse.Father = person.Spouse.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "father-child")?.PersonId1Navigation;
                if(person.Spouse.Father != null)
                {
                    person.Spouse.Father = _context.People
                        .Include(p => p.Photos)
                        .Include(p => p.RelationshipPersonId1Navigations)
                        .ThenInclude(p => p.PersonId2Navigation)
                        .Include(p => p.RelationshipPersonId2Navigations)
                        .ThenInclude(p => p.PersonId1Navigation)
                        .FirstOrDefault(p => p.Id == person.Spouse.Father.Id);
                }
                person.Spouse.Mother = person.Spouse.RelationshipPersonId2Navigations.FirstOrDefault(r => r.RelationshipType == "mother-child")?.PersonId1Navigation;
                if (person.Spouse.Mother != null)
                {
                    person.Spouse.Mother = _context.People
                        .Include(p => p.Photos)
                        .Include(p => p.RelationshipPersonId1Navigations)
                        .ThenInclude(p => p.PersonId2Navigation)
                        .Include(p => p.RelationshipPersonId2Navigations)
                        .ThenInclude(p => p.PersonId1Navigation)
                        .FirstOrDefault(p => p.Id == person.Spouse.Mother.Id);
                }
            }
            
            return View(person);
        }

        [Route("Tree/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Tree/Manage")]
        public IActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id", "FirstName", "LastName", "Gender", "MaidebName", "OtherNameVariants", "BirthDate",
            "BirthPlace", "DeathDate", "DeathPlace")] Person person)
        {
            Console.WriteLine(person);
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
    }
}
