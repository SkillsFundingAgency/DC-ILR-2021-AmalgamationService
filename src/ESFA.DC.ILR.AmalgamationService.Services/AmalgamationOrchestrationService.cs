using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationOrchestrationService : IAmalgamationOrchestrationService
    {
        IMessageProvider<Message> _messageProvider;
        IAmalgamationService _amalgamationService;
        IAmalgamationOutputService _amalgamationOutputService;
        public AmalgamationOrchestrationService(IMessageProvider<Message> messageProvider, IAmalgamationService amalgamationService, IAmalgamationOutputService amalgamationOutputService)
        {
            _messageProvider = messageProvider;
            _amalgamationService = amalgamationService;
            _amalgamationOutputService = amalgamationOutputService;
        }
        public async Task ProcessAsync(List<string> files, string outputPath, CancellationToken cancellationToken)
        {
            //TODO:file level pre validation here

            List<Message> messages = new List<Message>();

            foreach (var file in files)
            {
                var messaage = await _messageProvider.ProvideAsync(file, cancellationToken);
                messages.Add(messaage);
            }

            var amalgamatedMessage = await _amalgamationService.AmalgamateAsync(messages, cancellationToken);

            await _amalgamationOutputService.ProcessAsync(amalgamatedMessage, outputPath, cancellationToken);
        }
    }
}
