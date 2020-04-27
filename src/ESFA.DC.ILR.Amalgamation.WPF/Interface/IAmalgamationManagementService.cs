using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Amalgamation.WPF.Interface
{
    public interface IAmalgamationManagementService
    {
        Task<bool> ProcessAsync(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken);
    }
}
