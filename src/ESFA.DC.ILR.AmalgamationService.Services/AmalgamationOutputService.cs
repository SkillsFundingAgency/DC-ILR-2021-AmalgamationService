using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Model.Loose;
using ESFA.DC.Serialization.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationOutputService : IAmalgamationOutputService
    {
        private readonly IXmlSerializationService _xmlSerializationService;
        private readonly IFileService _fileService;
        public AmalgamationOutputService(
            IXmlSerializationService xmlSerializationService,
            IFileService fileService)
        {
            _xmlSerializationService = xmlSerializationService;
            _fileService = fileService;
        }

        public Task ProcessAsync(IAmalgamationResult amalgamatedMessage, string outputFilePath, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
