using Application.Extensions;
using Application.Interfaces.Demos;
using Application.Interfaces.Persistence;
using Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.EntityFrameworkDemos;

public class InterceptorsDemo : IEntityFrameworkFeatureDemo
{
    public string FeatureDescription => "Entity Framework Interceptors";

    public async Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation)
    {
        var order = await dbContext.SettlementOrders.FirstOrNotFoundAsync(o => o.Identifier == "Identifier_Seed_9", nameof(SettlementOrder), "Identifier_Seed_9", cancellation);
        order.SettlementStringFieldOne = "Changed Settlement String Field One";
        await dbContext.SaveChangesAsync(cancellation);
    }
}