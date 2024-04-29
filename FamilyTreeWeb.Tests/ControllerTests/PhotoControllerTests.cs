using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTreeWeb.Context;
using FamilyTreeWeb.Controllers;
using FamilyTreeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FamilyTreeWeb.Tests.ControllerTests
{
    public class PhotoControllerTests
    {
        private readonly DbContextOptions<FamilyTreeDbContext> _options;

        public PhotoControllerTests()
        {
            _options = new DbContextOptionsBuilder<FamilyTreeDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_FamilyTreeDB")
                .Options;
        }

        [Fact]
        public async Task Photo_ReturnsNotFound_WhenPhotoNotFound()
        {
            // Arrange
            using (var context = new FamilyTreeDbContext(_options))
            {
                var controller = new PhotoController(context);

                // Act
                var result = await controller.Photo(999); // Assuming photo with ID 999 doesn't exist

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Photo_ReturnsViewResult_WithPhoto()
        {
            // Arrange
            using (var context = new FamilyTreeDbContext(_options))
            {
                // Add sample photo to the in-memory database
                var photo = new Photo { Id = 1, FilePath = "sample.jpg" };
                await context.Photos.AddAsync(photo);
                await context.SaveChangesAsync();

                var controller = new PhotoController(context);

                // Act
                var result = await controller.Photo(1); // Assuming photo with ID 1 exists

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<Photo>(viewResult.ViewData.Model);
                Assert.Equal(photo.Id, model.Id);
            }
        }

        [Fact]
        public async Task Gallery_ReturnsNotFound_WhenTreeNotFound()
        {
            // Arrange
            using (var context = new FamilyTreeDbContext(_options))
            {
                var controller = new PhotoController(context);

                // Act
                var result = await controller.Gallery(999); // Assuming tree with ID 999 doesn't exist

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task Gallery_ReturnsViewResult_WithPhotos()
        {
            // Arrange
            using (var context = new FamilyTreeDbContext(_options))
            {
                // Add sample tree and related photos to the in-memory database
                var tree = new Tree { Id = 1, Name = "Sample Tree", Type = "public" };
                var photo1 = new Photo { Id = 1, FilePath = "sample1.jpg", PersonId = 1 };
                var photo2 = new Photo { Id = 2, FilePath = "sample2.jpg", PersonId = 2 };
                await context.Trees.AddAsync(tree);
                await context.Photos.AddRangeAsync(photo1, photo2);
                await context.SaveChangesAsync();

                var controller = new PhotoController(context);

                // Act
                var result = await controller.Gallery(1); // Assuming tree with ID 1 exists

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<List<Photo>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count); // Assuming there are 2 photos related to the tree
            }
        }
    }
}
