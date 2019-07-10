using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationService : IAmalgamationService
    {
        private readonly IAmalgamator<Message> _messageAmalgamator;

        public AmalgamationService(IAmalgamator<Message> messageAmalgamator)
        {
            _messageAmalgamator = messageAmalgamator;
        }

        public Task<IAmalgamationResult> AmalgamateAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            AmalgamationResult result = new AmalgamationResult();
            var message = _messageAmalgamator.Amalgamate(messages);
            result.Messaage = message;

            return Task.FromResult<IAmalgamationResult>(result);
        }
    }
}