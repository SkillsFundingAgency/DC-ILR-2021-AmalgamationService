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
    public class HeaderCollectionDetailsAmalgamator : AbstractAmalgamator<MessageHeaderCollectionDetails>, IAmalgamator<MessageHeaderCollectionDetails>
    {
        private IRule<string> _standardRuleString;
        private IRule<DateTime> _standardRuleDateTime;

        public HeaderCollectionDetailsAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.CollectionDetails, (x) => null, amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
        }

        public MessageHeaderCollectionDetails Amalgamate(IEnumerable<MessageHeaderCollectionDetails> models)
        {
            var msgHeaderCollection = new MessageHeaderCollectionDetails();

            ApplyRule(x => x.CollectionString, _standardRuleString.Definition, models, msgHeaderCollection);
            ApplyRule(x => x.YearString, _standardRuleString.Definition, models, msgHeaderCollection);
            ApplyRule(x => x.FilePreparationDate, _standardRuleDateTime.Definition, models, msgHeaderCollection);

            return msgHeaderCollection;
        }
    }
}
