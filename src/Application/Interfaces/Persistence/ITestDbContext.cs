using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Persistence
{
    public interface ITestDbContext : IDisposable
    {
        DbSet<IndividualConsumer> IndividualConsumers { get; set; }
        DbSet<OrganisationalConsumer> OrganisationalConsumers { get; set; }
        DbSet<SettlementOrder> SettlementOrders { get; set; }
        DbSet<Document> Documents { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
