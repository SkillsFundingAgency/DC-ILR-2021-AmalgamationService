using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Amalgamation.WPF.Interface
{
    public interface IAmalgamationManagementService
    {
        Task ProcessAsync(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken);
    }
}
