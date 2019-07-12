using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var amalgamationRoot in amalgamationRoots)
            {
                _parentRelationshipMapper.MapChildren(amalgamationRoot as IAmalgamationRoot);
            }

            var message = _messageAmalgamator.Amalgamate(amalgamationRoots.Select(r => r.Message as Message));
            result.Messaage = message as Message;

            return Task.FromResult<IAmalgamationResult>(result);
        }
    }
}