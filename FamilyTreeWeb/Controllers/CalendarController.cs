using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeWeb.Controllers
{
    public class CalendarController : Controller
    {
        private readonly FamilyTreeDbContext _context;

        public CalendarController(FamilyTreeDbContext context)
        {
            _context = context;
        }

        [Route("Calendar/{id}")]
        public async Task<IActionResult> Calendar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tree = await _context.Trees
                .Include(t => t.People)
                .ThenInclude(p => p.Photos)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tree == null)
            {
                return NotFound();
            }
            DateTime today = DateTime.Today;

            var people = tree.People.ToList();

            var anniversaries = new List<Anniversary>();

            foreach (var person in people)
            {
                if (person.BirthDate != null)
                {
                    // Річниця народження
                    DateTime birthday = new DateTime(person.BirthDate.Value.Year, person.BirthDate.Value.Month, person.BirthDate.Value.Day);

                    if (birthday.Month == today.Month && (birthday.Day <= today.Day + 5 && birthday.Day >= today.Day))
                    {
                        anniversaries.Add(new Anniversary
                        {
                            Person = person,
                            Date = birthday,
                            Type = AnniversaryType.Birth
                        });
                    }
                }

                if (person.DeathDate != null)
                {
                    // Річниця смерті
                    var deathAnniversary = new DateTime(person.DeathDate.Value.Year, person.DeathDate.Value.Month, person.DeathDate.Value.Day);
                    if (deathAnniversary.Month == today.Month && (deathAnniversary.Day <= today.Day + 5 && deathAnniversary.Day >= today.Day))
                    {
                        anniversaries.Add(new Anniversary
                        {
                            Person = person,
                            Date = deathAnniversary,
                            Type = AnniversaryType.Death
                        });
                    }
                }
            }

            return View(anniversaries);
        }
    }
}