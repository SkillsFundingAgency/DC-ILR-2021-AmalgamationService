using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class MessageAmalgamator : AbstractAmalgamator, IAmalgamator<Message>
    {
        private readonly IAmalgamator<MessageHeader> _headerAmalgamator;
        private readonly IAmalgamator<MessageLearner> _learnerAmalgamator;

        public MessageAmalgamator(IAmalgamator<MessageHeader> headerAmalgamator, IAmalgamator<MessageLearner> learnerAmalgamator)
            : base(Enum.GetName(typeof(Entity), Entity.Message), string.Empty)
        {
            _headerAmalgamator = headerAmalgamator;
            _learnerAmalgamator = learnerAmalgamator;
        }

        public Message Amalgamate(IEnumerable<Message> models)
        {
            var message = new Message();

            ApplyChildRule(m => m.Header, _headerAmalgamator, models, message);

            ApplyGroupedChildCollectionRule(m => m.Learner, g => g.LearnRefNumber, _learnerAmalgamator, models, message);

            return message;
        }
    }
}
