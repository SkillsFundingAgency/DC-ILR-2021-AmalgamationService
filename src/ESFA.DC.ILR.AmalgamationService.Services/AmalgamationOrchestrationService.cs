using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationOrchestrationService : IAmalgamationOrchestrationService
    {
        private readonly IMessageProvider<AmalgamationRoot> _messageProvider;
        private readonly IAmalgamationService _amalgamationService;
        private readonly IAmalgamationOutputService _amalgamationOutputService;
        private readonly IXsdValidationService _xsdValidationService;

        public AmalgamationOrchestrationService(
            IMessageProvider<AmalgamationRoot> messageProvider,
            IAmalgamationService amalgamationService,
            IAmalgamationOutputService amalgamationOutputService,
            IXsdValidationService xsdValidationService)
        {
            _messageProvider = messageProvider;
            _amalgamationService = amalgamationService;
            _amalgamationOutputService = amalgamationOutputService;
            _xsdValidationService = xsdValidationService;
        }

        public async Task<bool> ProcessAsync(IEnumerable<string> filePaths, string outputDirectory, CancellationToken cancellationToken)
        {
            // TODO:file level pre validation here
            List<AmalgamationRoot> amalgamationRoots = new List<AmalgamationRoot>();

            try
            {
                bool validSchema = true;

                foreach (var file in filePaths)
                {
                    validSchema = _xsdValidationService.ValidateSchema(file);

                    if (!validSchema)
                    {
                        break;
                    }
                }

                if (!validSchema)
                {
                    return false;
                }

                foreach (var file in filePaths)
                {
                    var amalgamationRoot = await _messageProvider.ProvideAsync(file, cancellationToken);
                    amalgamationRoots.Add(amalgamationRoot);
                }

                var amalgamationResult = await _amalgamationService.AmalgamateAsync(amalgamationRoots, cancellationToken);

                await _amalgamationOutputService.ProcessAsync(amalgamationResult, outputDirectory, cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                // TODO : do proper exception handling, logging etc.
                var error = ex;

                return false;
            }
        }
    }
}
