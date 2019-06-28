using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class HeaderAmalgamator : AbstractAmalgamator, IAmalgamator<MessageHeader>
    {
        private readonly IAmalgamator<MessageHeaderSource> _sourceAmalgamator;

        public HeaderAmalgamator(IAmalgamator<MessageHeaderSource> sourceAmalgamator)
            : base(Enum.GetName(typeof(Entity), Entity.Header), string.Empty)
        {
            _sourceAmalgamator = sourceAmalgamator;
        }

        public MessageHeader Amalgamate(IEnumerable<MessageHeader> models)
        {
            var header = new MessageHeader();

            ApplyChildRule(h => h.Source, _sourceAmalgamator, models, header);

            return header;
        }
    }
}
