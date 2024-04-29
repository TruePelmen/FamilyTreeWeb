using FamilyTreeWeb.Controllers;
using FamilyTreeWeb.Context;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FamilyTreeWeb.Tests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithListOfPublicTrees()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var dbContextOptions = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_FamilyTreeDB")
                .Options;

            using (var context = new FamilyTreeDbContext(dbContextOptions))
            {
                context.Trees.Add(new Tree { Id = 1, Type = "public", Name = "Tree 1" });
                context.Trees.Add(new Tree { Id = 2, Type = "private", Name = "Tree 2" });
                context.SaveChanges();
            }

            using (var context = new FamilyTreeDbContext(dbContextOptions))
            {
                var controller = new HomeController(loggerMock.Object, context);

                // Act
                var result = controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Tree>>(viewResult.ViewData.Model);
                Assert.Equal(1, model.Count()); // Перевіряємо, що в списку тільки одне публічне дерево
                Assert.Equal("Tree 1", model.First().Name); // Перевіряємо, що це публічне дерево має очікувану назву
            }
        }
    }
}
