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
    public class HeaderSourceAmalgamator : AbstractAmalgamator<MessageHeaderSource>, IAmalgamator<MessageHeaderSource>
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private IRule<DateTime> _standardRuleDateTime;
        private IRule<string> _standardRuleString;
        private IRule<int> _standardRuleInt;

        public HeaderSourceAmalgamator(IDateTimeProvider dateTimeProvider, IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.Source, (x) => null, amalgamationErrorHandler)
        {
            _dateTimeProvider = dateTimeProvider;
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleInt = ruleProvider.BuildStandardRule<int>();
        }

        public MessageHeaderSource Amalgamate(IEnumerable<MessageHeaderSource> models)
        {
            var source = new MessageHeaderSource() { DateTime = _dateTimeProvider.GetNowUtc() };

            ApplyRule(s => s.ProtectiveMarkingString, _standardRuleString.Definition, models, source);
            ApplyRule(s => s.UKPRN, _standardRuleInt.Definition, models, source);
            ApplyRule(s => s.SoftwareSupplier, _standardRuleString.Definition, models, source);
            ApplyRule(s => s.SoftwarePackage, _standardRuleString.Definition, models, source);
            ApplyRule(s => s.Release, _standardRuleString.Definition, models, source);
            ApplyRule(s => s.SerialNo, _standardRuleString.Definition, models, source);
            ApplyRule(s => s.ReferenceData, _standardRuleString.Definition, models, source);
            ApplyRule(s => s.ComponentSetVersion, _standardRuleString.Definition, models, source);

            return source;
        }
    }
}
