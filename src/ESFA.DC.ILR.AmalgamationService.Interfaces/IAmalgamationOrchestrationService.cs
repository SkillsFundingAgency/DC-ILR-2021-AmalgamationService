using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationOrchestrationService
    {
        Task ProcessAsync(List<string> files, CancellationToken cancellationToken);
    }
}
