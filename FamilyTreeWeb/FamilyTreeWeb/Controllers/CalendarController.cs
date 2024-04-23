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
                .ThenInclude(p => p.Events)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tree == null)
            {
                return NotFound();
            }
            var today = DateTime.Today;

            var people = tree.People.ToList();

            var anniversaries = new List<Anniversary>();

            //foreach (var person in people)
            //{
            //    if (person.BirthDate != null && person.DeathDate == null)
            //    {
            //        // Річниця народження
            //        var birthday = person.BirthDate.Value.ToDate(today.Year);
            //        if (birthday >= today && birthday <= today.AddDays(5))
            //        {
            //            anniversaries.Add(new Anniversary
            //            {
            //                Person = person,
            //                Date = birthday,
            //                Type = AnniversaryType.Birth
            //            });
            //        }
            //    }

            //    if (person.DeathDate != null)
            //    {
            //        // Річниця смерті
            //        var deathAnniversary = person.DeathDate.Value.ToDateTime(today.Year);
            //        if (deathAnniversary >= today && deathAnniversary <= today.AddDays(5))
            //        {
            //            anniversaries.Add(new Anniversary
            //            {
            //                Person = person,
            //                Date = deathAnniversary,
            //                Type = AnniversaryType.Death
            //            });
            //        }
            //    }
            //}

            return View(anniversaries);
        }
    }

    public enum AnniversaryType
    {
        Birth,
        Death
    }

    public class Anniversary
    {
        public Person Person { get; set; }
        public DateTime Date { get; set; }
        public AnniversaryType Type { get; set; }
    }
}