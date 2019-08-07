using System;
using System.Collections.Generic;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Mapper;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.Serialization.Interfaces;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationOutputService : IAmalgamationOutputService
    {
        private readonly IXmlSerializationService _xmlSerializationService;
        private readonly ICsvService _csvService;
        private readonly IFileService _fileService;

        public AmalgamationOutputService(
            IXmlSerializationService xmlSerializationService,
            IFileService fileService,
            ICsvService csvService)
        {
            _xmlSerializationService = xmlSerializationService;
            _fileService = fileService;
            _csvService = csvService;
        }

        public async Task ProcessAsync(IAmalgamationResult amalgamationResult, string outputDirectory, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(outputDirectory) && !Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var invalidLearnRefNumbers = GetInvalidLearnRefNumbers(amalgamationResult.ValidationErrors);

            var validMessage = RemoveInvalidLearnersFromMessage(amalgamationResult.Message, invalidLearnRefNumbers);

            if (validMessage != null)
            {
                // TODO derive file name from sourceFiles
                using (var stream = await _fileService.OpenWriteStreamAsync("AmalgamatedFile.xml", outputDirectory, cancellationToken))
                {
                    _xmlSerializationService.Serialize(validMessage, stream);
                }
            }

            if (amalgamationResult.ValidationErrors != null)
            {
                await _csvService.WriteAsync<IAmalgamationValidationError, AmalgamationValidationErrorMapper>(amalgamationResult.ValidationErrors, "AmalgamationValidationError.csv", outputDirectory, cancellationToken);
            }
        }

        private IEnumerable<string> GetInvalidLearnRefNumbers(IEnumerable<IAmalgamationValidationError> validationErrors)
        {
            return validationErrors.Where(ve => ve.Severity == Severity.Error).Select(ve => ve.LearnRefNumber).Distinct(StringComparer.OrdinalIgnoreCase);
        }

        private Message RemoveInvalidLearnersFromMessage(Message message, IEnumerable<string> invalidLearnRefNumbers)
        {
            var invalidLearnRefNumbersHashSet = new HashSet<string>(invalidLearnRefNumbers, StringComparer.OrdinalIgnoreCase);

            message.Learner = message?.Learner?.Where(l => !invalidLearnRefNumbersHashSet.Contains(l.LearnRefNumber)).ToArray();
            message.LearnerDestinationandProgression = message?.LearnerDestinationandProgression?.Where(dp => !invalidLearnRefNumbersHashSet.Contains(dp.LearnRefNumber)).ToArray();

            return message;
        }
    }
}
