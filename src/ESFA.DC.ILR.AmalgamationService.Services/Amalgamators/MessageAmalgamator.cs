using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class MessageAmalgamator : AbstractAmalgamator, IAmalgamator<Message>
    {
        private readonly IAmalgamator<MessageHeader> _headerAmalgamator;

        public MessageAmalgamator(IAmalgamator<MessageHeader> headerAmalgamator)
        {
            _headerAmalgamator = headerAmalgamator;
        }

        public Message Amalgamate(IEnumerable<Message> models)
        {
            var message = new Message();

            ApplyChildRule(m => m.Header, _headerAmalgamator, models, message);

            return message;
        }
    }
}
