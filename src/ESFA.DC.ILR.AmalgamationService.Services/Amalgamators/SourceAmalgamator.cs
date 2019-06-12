using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class SourceAmalgamator : AbstractAmalgamator, IAmalgamator<MessageHeaderSource>
    {
        public MessageHeaderSource Amalgamate(IEnumerable<MessageHeaderSource> models)
        {
            var source = new MessageHeaderSource();

            ApplyRule(s => s.DateTime, new FirstRule<DateTime>().Definition, models, source);

            return source;
        }
    }
}
