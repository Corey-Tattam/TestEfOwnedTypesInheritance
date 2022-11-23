﻿using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Benchmarks
{
    public abstract class EntityFrameworkBenchMarkBase
    {
        protected static TestDbContext CreateContext() =>
            new TestDbContext(
                new DbContextOptionsBuilder<TestDbContext>()
                    .UseSqlServer("Server=.\\SQL2017;Database=TestEfOwnedInheritance;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options);
    }
}