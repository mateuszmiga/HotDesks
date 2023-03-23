using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Hotdesks.Tests
{
    public class GenericRepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public GenericRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task GetOwners_WhenCall_ShouldReturnOwnersAsync()
        {
            //Arrange
            var mock = new Mock<Context>();
            mock.Setup(x => x.Set<Owner>()).ReturnsDbSet(GetFakeOwners());
            var repo = new GenericRepository<Owner>(mock.Object);

            //Act
            var result =await repo.GetAll();
            foreach (var item in result)
            {
                _output.WriteLine(item.Name);
            }
            //Assert
            result.Should().AllBeOfType<Owner>().Should().NotBeNull();
        }
        
        
        [Fact]
        public async Task AddAsync_AnyEntities_ShouldBeAdded()
        {
            //Arrange
            var mockContext = new Mock<Context>();
            var mockSet = new Mock<DbSet<Owner>>();            
            mockContext.Setup(c => c.Set<Owner>()).Returns(mockSet.Object);
            var repo = new GenericRepository<Owner>(mockContext.Object);
            var owner = new Owner();
            owner.Name = "Test";

            //Act
            await repo.Create(owner);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Owner>()), Times.Once());            
            
            //mockContext.Verify(m => m.SaveChangesAsync(), Times.Once);
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
