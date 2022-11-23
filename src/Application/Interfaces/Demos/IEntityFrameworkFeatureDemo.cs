using Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Demos
{
    public interface IEntityFrameworkFeatureDemo
    {
        string FeatureDescription { get; }

        Task RunAsync(ITestDbContext dbContext, CancellationToken cancellation);
    }
}
