using FamilyTreeWeb.Controllers;
using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FamilyTreeWeb.Tests.ControllerTests
{
    public class CalendarControllerTests
    {
        //[Fact]
        //public async Task Calendar_ReturnsViewResult_WithAnniversariesList()
        //{
        //    // Arrange
        //    var fixedDate1 = new DateOnly(2000, 1, 1); // Фіксована дата для тесту
        //    var fixedDate2 = new DateTime(2000, 1, 1); // Фіксована дата для тесту
        //    var anniversaries = new List<Anniversary>
        //    {
        //        new Anniversary { Person = new Person { Id = 1, FirstName = "John", LastName = "Doe", BirthDate = fixedDate1, DeathDate = null }, Date = fixedDate2, Type = AnniversaryType.Birth },
        //        new Anniversary { Person = new Person { Id = 2, FirstName = "Jane", LastName = "Doe", BirthDate = null, DeathDate = fixedDate1 }, Date = fixedDate2, Type = AnniversaryType.Death }
        //    };

        //    var dbContextOptions = new DbContextOptionsBuilder<FamilyTreeDbContext>()
        //        .UseInMemoryDatabase(databaseName: "Test_FamilyTreeDB")
        //        .Options;

        //    using (var context = new FamilyTreeDbContext(dbContextOptions))
        //    {
        //        // Додати тестові дані в базу даних
        //        await context.AddRangeAsync(anniversaries.Select(a => a.Person));
        //        await context.SaveChangesAsync();
        //    }

        //    using (var context = new FamilyTreeDbContext(dbContextOptions))
        //    {
        //        var controller = new CalendarController(context);

        //        // Act
        //        var result = await controller.Calendar(1);

        //        // Assert
        //        var viewResult = Assert.IsType<ViewResult>(result);
        //        var model = Assert.IsAssignableFrom<IEnumerable<Anniversary>>(viewResult.ViewData.Model);
        //        Assert.Equal(2, model.Count()); // Перевіряємо, що в списку дві річниці
        //        Assert.Contains(model, a => a.Person.FirstName == "John" && a.Type == AnniversaryType.Birth); // Перевіряємо, що в списку є річниця народження для особи John
        //        Assert.Contains(model, a => a.Person.FirstName == "Jane" && a.Type == AnniversaryType.Death); // Перевіряємо, що в списку є річниця смерті для особи Jane
        //    }
        //}

        [Fact]
        public async Task Calendar_ReturnsNotFound_WhenTreeNotFound()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_FamilyTreeDB")
                .Options;

            using (var context = new FamilyTreeDbContext(dbContextOptions))
            {
                var controller = new CalendarController(context);

                // Act
                var result = await controller.Calendar(100); // Передаємо ідентифікатор, який не існує

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}
