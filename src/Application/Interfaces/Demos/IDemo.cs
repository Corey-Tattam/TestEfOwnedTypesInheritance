using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Demos
{
    public interface IDemo
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}
