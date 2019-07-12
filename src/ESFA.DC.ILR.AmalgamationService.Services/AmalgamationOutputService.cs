using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Mapper;
using ESFA.DC.Serialization.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationOutputService : IAmalgamationOutputService
    {
        private readonly IXmlSerializationService _xmlSerializationService;
        private readonly ICsvService _csvService;

        public AmalgamationOutputService(
            IXmlSerializationService xmlSerializationService,
            ICsvService csvService)
        {
            _xmlSerializationService = xmlSerializationService;
            _csvService = csvService;
        }

        public async Task ProcessAsync(IAmalgamationResult amalgamationResult, string outputFilePath, CancellationToken cancellationToken)
        {
            if (amalgamationResult.ValidationErrors != null)
            {
                await _csvService.WriteAsync<IAmalgamationValidationError, AmalgamationValidationErrorMapper>(amalgamationResult.ValidationErrors, "AmalgamationValidationError.csv", outputFilePath, cancellationToken);
            }
        }
    }
}
