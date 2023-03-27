using Bogus;
using Bogus.DataSets;
using Data.EFCore.Repository;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Hotdesks.Tests
{
    public class GenericRepositoryTestsWithInMemoryDb : IClassFixture<InMemoryDb>
    {
        private readonly InMemoryDb _db;        
        private readonly ITestOutputHelper _output;

        public GenericRepositoryTestsWithInMemoryDb(InMemoryDb db, ITestOutputHelper output)
        {
            _db = db;
            _output = output;
        }

        [Fact]
        public async Task Create_OneOwner_CreateNewOwnerAsync()
        {
            //Arrange
            var repo = new GenericRepository<Owner>(_db);
            var owner = new Faker<Owner>().RuleFor(n => n.Name, o => o.Person.FullName).Generate();
            
            //Act
            await repo.Create(owner);
            var result = _db.Owners.First().Name;
            _output.WriteLine(result);
            
            //Assert
            _db.Owners.Should().HaveCount(1);
            result.Should().Be(owner.Name);
        }
    }
}
