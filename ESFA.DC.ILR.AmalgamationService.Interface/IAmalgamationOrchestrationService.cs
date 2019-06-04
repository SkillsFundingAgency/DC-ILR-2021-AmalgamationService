using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Interface
{
    public interface IAmalgamationOrchestrationService<U>
    {
        Task ExecuteAsync(IAmalgamationContext amalgamationContext, CancellationToken cancellationToken);
    }
}