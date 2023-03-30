using Bogus;
using Data.EFCore.DbContext;
using Data.EFCore.Repository;
using Domain.Entities;
using FluentAssertions;
using System.Linq;
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
            DataSeeder seeder = new DataSeeder();
            seeder.SeedDataAsyncIfDbIsEmpty(db);
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
            var result = _db.Owners.First(o => o.Name == owner.Name).Name;
            _output.WriteLine(result);

            //Assert            
            result.Should().Be(owner.Name);
        }

        [Fact]
        public async Task GetAll_WhenCalled_ShouldReturnAllEntitiesInTable()
        {
            //Arrange
            var repo = new GenericRepository<Owner>(_db);

            //Act
            var result = await repo.GetAll();

            //Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Delete_SpecifiedOwner_ShouldDeleteSpecifiedEntity()
        {
            //arrange
            var repo = new GenericRepository<Owner>(_db);
            var entityCount = _db.Owners.Count();
            var owner = _db.Owners.First(_o => _o.Id == 1);

            //act
            await repo.Delete(owner);

            //assert
            _db.Owners.Count().Should().Be(entityCount - 1);
            _db.Owners.Any(o => o.Name == owner.Name).Should().BeFalse();
        }

        [Fact]
        public async Task Update_OwnerName_ShouldUpdateSpecifiedEntity()
        {
            //arrange
            var repo = new GenericRepository<Owner>(_db);
            var owner = _db.Owners.First(_o => _o.Id == 2);
            var oldName = owner.Name;
            owner.Name = "test test";

            //act
            await repo.UpdateAsync(owner);

            //assert            
            _db.Owners.Any(o => o.Name == oldName).Should().BeFalse();
            _db.Owners.Any(o => o.Name == owner.Name).Should().BeTrue();
        }

        [Fact]
        public async Task GetById_Id_ShouldReturnSpecifiedEntity()
        {
            //arrange
            var repo = new GenericRepository<Owner>(_db);

            //act
            var owner = await repo.GetByIdAsync(2);

            //assert            
            owner.Should().NotBeNull();
            owner.Id.Should().Be(2);
            owner.Name.Should().NotBeEmpty();
        }
    }
}
