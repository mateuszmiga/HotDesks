using AutoFixture;
using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
    public class GenericRepositoryTestsWithMoq
    {
        private readonly ITestOutputHelper _output;

        public GenericRepositoryTestsWithMoq(ITestOutputHelper output)
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
            var fixture = new Fixture();

            //This code is needed to support recursion
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            var mockContext = new Mock<Context>();
            var mockSet = new Mock<DbSet<Owner>>();            
            mockContext.Setup(c => c.Set<Owner>()).Returns(mockSet.Object);
            var repo = new GenericRepository<Owner>(mockContext.Object);
            var owner = fixture.Create<Owner>();            

            //Act
            await repo.Create(owner);

            //Assert
            mockSet.Verify(m => m.Add(It.IsAny<Owner>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        //[Fact]
        //public async Task Delete_WhenCalled_ShouldDeleteSpecifiedEntity()
        //{
        //    //Arrange
        //    var owners = GetFakeOwners();
        //    var mockContext = new Mock<Context>();
        //    var mockSet = new Mock<DbSet<Owner>>();
        //    //mockSet.Setup(c => c.AddRange(It.IsAny<List<Owner>>()));
        //    mockContext.Setup(c => c.Set<Owner>().AddRange(owners));
        //    var repo = new GenericRepository<Owner>(mockContext.Object);

        //    //Act
        //    var result = repo.Delete(mockContext.Object.Owners.First());

        //    //Assert
        //    mockSet.Verify(c => c.Remove(It.IsAny<Owner>()), Times.Once());
        //    mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);

        //}


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
