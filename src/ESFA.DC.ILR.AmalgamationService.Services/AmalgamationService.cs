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
        private readonly IAmalgamationErrorHandler _amalgamationErrorHandler;

        public AmalgamationService(IAmalgamator<Message> messageAmalgamator, IParentRelationshipMapper parentRelationshipMapper, IAmalgamationErrorHandler amalgamationErrorHandler)
        {
            _messageAmalgamator = messageAmalgamator;
            _parentRelationshipMapper = parentRelationshipMapper;
            _amalgamationErrorHandler = amalgamationErrorHandler;
        }

        public Task<IAmalgamationResult> AmalgamateAsync(IEnumerable<AmalgamationRoot> amalgamationRoots, CancellationToken cancellationToken)
        {
            foreach (var amalgamationRoot in amalgamationRoots)
            {
                _parentRelationshipMapper.MapChildren(amalgamationRoot as IAmalgamationRoot);
            }

            var message = _messageAmalgamator.Amalgamate(amalgamationRoots.Select(r => r.Message as Message));
            AmalgamationResult result = new AmalgamationResult()
            {
                Messaage = message as Message,
                ValidationErrors = _amalgamationErrorHandler.Errors
            };

            return Task.FromResult<IAmalgamationResult>(result);
        }
    }
}