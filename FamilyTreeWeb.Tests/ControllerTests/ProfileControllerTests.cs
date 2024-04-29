using System;
using System.Threading.Tasks;
using FamilyTreeWeb.Controllers;
using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FamilyTreeWeb.Tests.ControllerTests
{
    public class ProfileControllerTests
    {
        private readonly DbContextOptions<FamilyTreeDbContext> _options;

        public ProfileControllerTests()
        {
            _options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_FamilyTreeDB")
                .Options;
        }

        //[Fact]
        //public async Task Profile_ReturnsNotFound_WhenPersonNotFound()
        //{
        //    // Arrange
        //    using (var context = new FamilyTreeDbContext(_options))
        //    {
        //        var controller = new ProfileController(context);

        //        // Act
        //        var result = await controller.Profile(999); // Assuming person with ID 999 doesn't exist

        //        // Assert
        //        Assert.IsType<NotFoundResult>(result);
        //    }
        //}


        [Fact]
        public async Task Profile_ReturnsViewResult_WithPerson()
        {
            // Arrange
            using (var context = new FamilyTreeDbContext(_options))
            {
                // Add sample person to the in-memory database
                var person = new Person { Id = 1, FirstName = "John", LastName = "Doe", Gender = "male" }; // Ensure all required properties are set
                await context.People.AddAsync(person);
                await context.SaveChangesAsync();

                var controller = new ProfileController(context);

                // Act
                var result = await controller.Profile(1); // Assuming person with ID 1 exists

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Person>(viewResult.ViewData.Model);
                Assert.Equal(person.Id, model.Id);
            }
        }
    }
}
