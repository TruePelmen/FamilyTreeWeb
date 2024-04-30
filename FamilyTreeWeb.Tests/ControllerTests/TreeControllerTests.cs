using FamilyTreeWeb.Controllers;
using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FamilyTreeWeb.Tests.ControllerTests
{
    public class TreeControllerTests
    {
        private readonly DbContextOptions<FamilyTreeDbContext> _options;

        public TreeControllerTests()
        {
            _options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                           .UseInMemoryDatabase(databaseName: "Test_FamilyTreeDB")
                           .Options;
        }

        [Fact]
        public void Create_Returns_ViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new FamilyTreeDbContext(options))
            {
                var controller = new TreeController(context);

                // Act
                var result = controller.Create();

                // Assert
                Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public void Create_Returns_View_With_No_Parameters()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new FamilyTreeDbContext(options))
            {
                var controller = new TreeController(context);

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                Assert.Null(result.ViewName);
            }
        }

        [Fact]
        public async Task Create_Post_Returns_RedirectToActionResult_When_Model_Is_Valid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new FamilyTreeDbContext(options))
            {
                var controller = new TreeController(context);
                var tree = new Tree { Id = 1, Name = "Test Tree", PrimaryPerson = 1, Type = "Test" };

                // Act
                var result = await controller.Create(tree) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("ViewTree", result.ActionName);
                Assert.Equal(1, result.RouteValues["id"]);
            }
        }

        [Fact]
        public async Task Create_Post_Returns_ViewResult_With_Same_Model_When_Model_Is_Invalid()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Database")
                .Options;

            using (var context = new FamilyTreeDbContext(options))
            {
                var controller = new TreeController(context);
                var invalidTree = new Tree(); // Invalid model without required properties
                controller.ModelState.AddModelError("Name", "Name is required");

                // Act
                var result = await controller.Create(invalidTree) as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(invalidTree, result.Model);
            }
        }



    }
}
