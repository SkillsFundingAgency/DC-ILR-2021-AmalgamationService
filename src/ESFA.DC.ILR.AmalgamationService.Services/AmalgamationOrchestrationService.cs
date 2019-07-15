using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationOrchestrationService : IAmalgamationOrchestrationService
    {
        private IMessageProvider<AmalgamationRoot> _messageProvider;
        private IAmalgamationService _amalgamationService;
        private IAmalgamationOutputService _amalgamationOutputService;

        public AmalgamationOrchestrationService(IMessageProvider<AmalgamationRoot> messageProvider, IAmalgamationService amalgamationService, IAmalgamationOutputService amalgamationOutputService)
        {
            _messageProvider = messageProvider;
            _amalgamationService = amalgamationService;
            _amalgamationOutputService = amalgamationOutputService;
        }

        public async Task ProcessAsync(IEnumerable<string> filePaths, string outputDirectory, CancellationToken cancellationToken)
        {
            // TODO:file level pre validation here
            List<AmalgamationRoot> amalgamationRoots = new List<AmalgamationRoot>();

            foreach (var file in filePaths)
            {
                var amalgamationRoot = await _messageProvider.ProvideAsync(file, cancellationToken);
                amalgamationRoots.Add(amalgamationRoot);
            }

            var amalgamationResult = await _amalgamationService.AmalgamateAsync(amalgamationRoots, cancellationToken);

            await _amalgamationOutputService.ProcessAsync(amalgamationResult, outputDirectory, cancellationToken);
        }
    }
}
