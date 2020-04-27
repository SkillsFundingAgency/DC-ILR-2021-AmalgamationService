using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationOrchestrationService
    {
        Task<bool> ProcessAsync(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken);
    }
}
