using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class ContactPreferenceAmalgamator : AbstractAmalgamator<MessageLearnerContactPreference>, IAmalgamator<MessageLearnerContactPreference>
    {
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public ContactPreferenceAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerEmploymentStatus, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerContactPreference Amalgamate(IEnumerable<MessageLearnerContactPreference> models)
        {
            var messageLearnerContactPreference = new MessageLearnerContactPreference();

            ApplyRule(s => s.ContPrefCodeNullable, _standardRuleLong.Definition, models, messageLearnerContactPreference);
            ApplyRule(s => s.ContPrefType, _standardRuleString.Definition, models, messageLearnerContactPreference);

            return messageLearnerContactPreference;
        }
    }
}
