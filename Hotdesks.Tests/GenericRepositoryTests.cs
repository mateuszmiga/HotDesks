using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotdesks.Tests
{
    public class GenericRepositoryTests
    {
        [Fact]
        public async Task GetOwners_WhenCall_ShouldReturnOwnersAsync()
        {
            //Arrange
            var mock = new Mock<Context>();
            mock.Setup(x => x.Set<Owner>()).ReturnsDbSet(GetFakeOwners());
            var repo = new GenericRepository<Owner>(mock.Object);

            //Act
            var result =await repo.GetAll();

            //Assert
            result.Should().AllBeOfType<Owner>().Should().NotBeNull();
        }


        private static List<Owner> GetFakeOwners()
        {
            return new List<Owner>
            {
                new Owner { Id = 1, Name = "John Smith" },
                new Owner { Id = 2, Name = "Jane Doe" },
                new Owner { Id = 3, Name = "Bob Johnson" },
                new Owner { Id = 4, Name = "Samantha Williams" }
            };
        }
    }
}
