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
    }
}
