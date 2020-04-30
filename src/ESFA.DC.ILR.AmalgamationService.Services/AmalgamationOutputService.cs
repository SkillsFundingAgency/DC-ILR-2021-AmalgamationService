using System;
using System.Collections.Generic;
using ESFA.DC.DateTimeProvider.Interface;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IFileService _fileService;

        public AmalgamationOutputService(
            IXmlSerializationService xmlSerializationService,
            IFileService fileService,
            ICsvService csvService,
            IDateTimeProvider dateTimeProvider)
        {
            _xmlSerializationService = xmlSerializationService;
            _fileService = fileService;
            _csvService = csvService;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task ProcessAsync(IEnumerable<IValidationError> validationErrors, string outputDirectory, CancellationToken cancellationToken)
        {
            await _csvService.WriteAsync<IValidationError, ValidationErrorMapper>(validationErrors, $"Rule Violation Report.csv", outputDirectory, cancellationToken);
        }

        public async Task ProcessAsync(IAmalgamationResult amalgamationResult, string outputDirectory, CancellationToken cancellationToken)
        {
            var invalidLearnRefNumbers = GetInvalidLearnRefNumbers(amalgamationResult.ValidationErrors);

            var validMessage = RemoveInvalidLearnersFromMessage(amalgamationResult.Message, invalidLearnRefNumbers);

            if (validMessage != null)
            {
                var amalgamatedFileName = $"ILR-{amalgamationResult.Message.Header.Source.UKPRN}-2021-{_dateTimeProvider.GetNowUtc().ToString("yyyyMMdd-HHmmss")}-01.xml";
                using (var stream = await _fileService.OpenWriteStreamAsync(amalgamatedFileName, outputDirectory, cancellationToken))
                {
                    _xmlSerializationService.Serialize(validMessage, stream);
                }
            }

            if (amalgamationResult.ValidationErrors != null)
            {
                await _csvService.WriteAsync<IAmalgamationValidationError, AmalgamationValidationErrorMapper>(amalgamationResult.ValidationErrors, $"FileMergeSummaryReport.csv", outputDirectory, cancellationToken);
            }
        }

        private IEnumerable<string> GetInvalidLearnRefNumbers(IEnumerable<IAmalgamationValidationError> validationErrors)
        {
            return validationErrors.Where(ve => ve.Severity == Severity.Error).Select(ve => ve.LearnRefNumber).Distinct(StringComparer.OrdinalIgnoreCase);
        }

        private Message RemoveInvalidLearnersFromMessage(Message message, IEnumerable<string> invalidLearnRefNumbers)
        {
            var invalidLearnRefNumbersHashSet = new HashSet<string>(invalidLearnRefNumbers, StringComparer.OrdinalIgnoreCase);

            message.Learner.ForEach(l =>
            {
                if (invalidLearnRefNumbersHashSet.Contains(l.LearnRefNumber))
                {
                    message.Learner.Remove(l);
                }
            });
            //message.Learner = message?.Learner?.Where(l => !invalidLearnRefNumbersHashSet.Contains(l.LearnRefNumber)).ToArray();

            message.LearnerDestinationandProgression.ForEach(l =>
            {
                if (invalidLearnRefNumbersHashSet.Contains(l.LearnRefNumber))
                {
                    message.LearnerDestinationandProgression.Remove(l);
                }
            });
            //message.LearnerDestinationandProgression = message?.LearnerDestinationandProgression?.Where(dp => !invalidLearnRefNumbersHashSet.Contains(dp.LearnRefNumber)).ToArray();

            return message;
        }
    }
}
