using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationOutputService
    {
        Task ProcessAsync(IAmalgamationResult amalgamationResult, string outputFilePath, CancellationToken cancellationToken);
    }
}
