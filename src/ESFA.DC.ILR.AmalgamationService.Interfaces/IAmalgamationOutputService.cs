using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ESFA.DC.ILR.Model.Interface;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationOutputService
    {
        Task ProcessAsync(IAmalgamationResult amalgamationResult, string outputFilePath, CancellationToken cancellationToken);
    }
}
