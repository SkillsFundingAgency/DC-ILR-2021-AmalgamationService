using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationOutputService
    {
        Task ProcessAsync(IEnumerable<IValidationError> validationErrors, string outputDirectory, CancellationToken cancellationToken);

        Task ProcessAsync(IAmalgamationResult amalgamationResult, string outputFilePath, CancellationToken cancellationToken);
    }
}
