using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Mapper;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.Serialization.Interfaces;
using System.IO;
using System.Text;
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

            if (amalgamationResult.Message != null)
            {
                // TODO derive file name from sourceFiles
                using (var stream = await _fileService.OpenWriteStreamAsync("AmalgamatedFile.xml", outputDirectory, cancellationToken))
                {
                    var message = _xmlSerializationService.Serialize<Message>(amalgamationResult.Message);
                    var messageByte = Encoding.ASCII.GetBytes(message);
                    await stream.WriteAsync(messageByte, 0, messageByte.Length, cancellationToken);
                }
            }

            if (amalgamationResult.ValidationErrors != null)
            {
                await _csvService.WriteAsync<IAmalgamationValidationError, AmalgamationValidationErrorMapper>(amalgamationResult.ValidationErrors, "AmalgamationValidationError.csv", outputDirectory, cancellationToken);
            }
        }
    }
}
