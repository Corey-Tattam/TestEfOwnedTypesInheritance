using System.Threading.Tasks;

namespace ConsoleUi.Interfaces
{
    public interface IAppHost
    {
        Task<int> RunAsync(string[] args);
    }
}
