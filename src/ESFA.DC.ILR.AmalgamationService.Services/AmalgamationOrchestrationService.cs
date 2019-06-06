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
        public Task ProcessAsync(List<string> files, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
