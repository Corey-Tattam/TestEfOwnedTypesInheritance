using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Demos
{
    public interface IDemoCollectionRunner
    {
        Task RunAllAsync(CancellationToken cancellationToken);
    }
}
