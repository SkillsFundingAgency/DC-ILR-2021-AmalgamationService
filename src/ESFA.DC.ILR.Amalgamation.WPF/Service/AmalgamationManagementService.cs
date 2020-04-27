using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class AmalgamationManagementService : IAmalgamationManagementService
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AmalgamationManagementService(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task<bool> ProcessAsync(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken)
        {
            return await Task.Run(() => ExecuteAsyncAction(filePaths, outputPath, cancellationToken), cancellationToken);
        }

        private async Task<bool> ExecuteAsyncAction(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken)
        {
            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope())
            {
                return await executionLifetimeScope.Resolve<IAmalgamationOrchestrationService>().ProcessAsync(filePaths, outputPath, cancellationToken);
            }
        }
    }
}
