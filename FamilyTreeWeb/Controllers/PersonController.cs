using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeWeb.Controllers
{
    public class PersonController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public PersonController(FamilyTreeDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            ViewData["PeopleList"] = new SelectList(_context.People, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FirstName, LastName, Gender, BirthDate, DeathDate, BirthPlace, DeathPlace, IdTree, FatherId, MotherId, SpouseId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PeopleList"] = new SelectList(_context.People, "Id", "FullName", person.Id);
            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            ViewData["PeopleList"] = new SelectList(_context.People, "Id", "FullName", person.Id);
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, FirstName, LastName, Gender, BirthDate, DeathDate, BirthPlace, DeathPlace, IdTree, FatherId, MotherId, SpouseId")] Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.People.Any(e => e.Id == person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewData["PeopleList"] = new SelectList(_context.People, "Id", "FullName", person.Id);
            return View(person);
        }
    }
}

