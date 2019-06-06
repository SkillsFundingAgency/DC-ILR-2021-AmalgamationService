using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IMessageProvider<T>
    {
        Task<T> ProvideAsync(string filepath, CancellationToken cancellationToken);
    }
}
