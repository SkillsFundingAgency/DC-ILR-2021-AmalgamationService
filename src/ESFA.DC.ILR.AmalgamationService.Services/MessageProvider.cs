using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose;
using ESFA.DC.Serialization.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class MessageProvider : IMessageProvider<Message>
    {
        private readonly IXmlSerializationService _xmlSerializationService;
        private readonly IFileService _fileService;
        public MessageProvider(
            IXmlSerializationService xmlSerializationService,
            IFileService fileService)
        {
            _xmlSerializationService = xmlSerializationService;
            _fileService = fileService;
        }
        public async Task<Message> ProvideAsync(string filepath, CancellationToken cancellationToken)
        {
            string filename = Path.GetFileName(filepath);
            string container = Path.GetDirectoryName(filepath);
            using (var stream = await _fileService.OpenReadStreamAsync(filename, container, cancellationToken))
            {
                return _xmlSerializationService.Deserialize<Message>(stream);
            }
        }
    }
}
