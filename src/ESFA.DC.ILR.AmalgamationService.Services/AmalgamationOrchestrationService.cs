using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IValidationErrorHandler _validationErrorHandler;
        private readonly ICrossValidationService _crossValidationService;

        public AmalgamationOrchestrationService(
            IMessageProvider<AmalgamationRoot> messageProvider,
            IAmalgamationService amalgamationService,
            IAmalgamationOutputService amalgamationOutputService,
            IXsdValidationService xsdValidationService,
            IDateTimeProvider dateTimeProvider,
            IValidationErrorHandler validationErrorHandler,
            ICrossValidationService crossValidationService)
        {
            _messageProvider = messageProvider;
            _amalgamationService = amalgamationService;
            _amalgamationOutputService = amalgamationOutputService;
            _xsdValidationService = xsdValidationService;
            _dateTimeProvider = dateTimeProvider;
            _validationErrorHandler = validationErrorHandler;
            _crossValidationService = crossValidationService;
        }

        public async Task<bool> ProcessAsync(IEnumerable<string> filePaths, string outputDirectory, CancellationToken cancellationToken)
        {
            // TODO:file level pre validation here
            List<AmalgamationRoot> amalgamationRoots = new List<AmalgamationRoot>();

            try
            {
                if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                var outputDirectoryForInstance = $"{outputDirectory}/Amalgamation-{_dateTimeProvider.GetNowUtc().ToString("yyyyMMdd-HHmmss")}";
                Directory.CreateDirectory(outputDirectoryForInstance);

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
                    await _amalgamationOutputService.ProcessAsync(_validationErrorHandler.ValidationErrors, outputDirectoryForInstance, cancellationToken);
                    return false;
                }

                foreach (var file in filePaths)
                {
                    var amalgamationRoot = await _messageProvider.ProvideAsync(file, cancellationToken);
                    amalgamationRoot.Message = _crossValidationService.CrossValidateLearners(amalgamationRoot.Message);
                    amalgamationRoots.Add(amalgamationRoot);
                }

                var amalgamationResult = await _amalgamationService.AmalgamateAsync(amalgamationRoots, cancellationToken);

                await _amalgamationOutputService.ProcessAsync(amalgamationResult, outputDirectoryForInstance, cancellationToken);

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
