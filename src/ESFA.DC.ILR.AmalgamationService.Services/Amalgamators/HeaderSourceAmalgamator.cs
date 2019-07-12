using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class HeaderSourceAmalgamator : AbstractAmalgamator<MessageHeaderSource>, IAmalgamator<MessageHeaderSource>
    {
        private IRule<DateTime> _standardRuleDateTime;

        public HeaderSourceAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.Source, (x) => null, amalgamationErrorHandler)
        {
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
        }

        public MessageHeaderSource Amalgamate(IEnumerable<MessageHeaderSource> models)
        {
            var source = new MessageHeaderSource();

            ApplyRule(s => s.DateTime, _standardRuleDateTime.Definition, models, source);

            return source;
        }
    }
}
