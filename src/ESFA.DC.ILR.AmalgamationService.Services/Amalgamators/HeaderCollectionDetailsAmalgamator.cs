using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class HeaderCollectionDetailsAmalgamator : AbstractAmalgamator<MessageHeaderCollectionDetails>, IAmalgamator<MessageHeaderCollectionDetails>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private IRule<string> _standardRuleString;
        private IRule<DateTime> _standardRuleDateTime;

        public HeaderCollectionDetailsAmalgamator(IDateTimeProvider dateTimeProvider, IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.CollectionDetails, (x) => null, amalgamationErrorHandler)
        {
            _dateTimeProvider = dateTimeProvider;
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
        }

        public MessageHeaderCollectionDetails Amalgamate(IEnumerable<MessageHeaderCollectionDetails> models)
        {
            return new MessageHeaderCollectionDetails() { FilePreparationDate = _dateTimeProvider.GetNowUtc() };
        }
    }
}
