using Data.EFCore.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotdesks.Tests
{
    internal class RepositoryTests
    {
        private readonly DbContextOptions<Context> _contextOptions;

        public RepositoryTests()
        {
            _contextOptions = new DbContextOptionsBuilder<Context>()
               .UseInMemoryDatabase("TestDB")
               .Options;

        }
    }
}
