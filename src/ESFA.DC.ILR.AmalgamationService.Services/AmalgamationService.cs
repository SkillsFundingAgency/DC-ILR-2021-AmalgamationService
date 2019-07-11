using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationService : IAmalgamationService
    {
        private readonly IAmalgamator<Message> _messageAmalgamator;
        private readonly IParentRelationshipMapper _parentRelationshipMapper;

        public AmalgamationService(IAmalgamator<Message> messageAmalgamator, IParentRelationshipMapper parentRelationshipMapper)
        {
            _messageAmalgamator = messageAmalgamator;
            _parentRelationshipMapper = parentRelationshipMapper;
        }

        public Task<IAmalgamationResult> AmalgamateAsync(IEnumerable<AmalgamationRoot> amalgamationRoots, CancellationToken cancellationToken)
        {
            AmalgamationResult result = new AmalgamationResult();
            List<Message> messages = new List<Message>();
            foreach (var amalgamationRoot in amalgamationRoots)
            {
                var mappedAmalgamationRoot = _parentRelationshipMapper.MapChildren(amalgamationRoot);
                messages.Add(mappedAmalgamationRoot.Message);
            }

            var message = _messageAmalgamator.Amalgamate(messages);
            result.Messaage = message;

            return Task.FromResult<IAmalgamationResult>(result);
        }
    }
}