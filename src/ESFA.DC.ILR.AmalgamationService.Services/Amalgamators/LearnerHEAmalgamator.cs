using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerHEAmalgamator : AbstractAmalgamator<MessageLearnerLearnerHE>, IAmalgamator<MessageLearnerLearnerHE>
    {
        private IRule<string> _standardRuleString;
        private IRule<long> _standardRuleLong;
        private IRule<DateTime> _standardRuleDateTime;

        public LearnerHEAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerEmploymentStatus, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
        }

        public MessageLearnerLearnerHE Amalgamate(IEnumerable<MessageLearnerLearnerHE> models)
        {
            var messageLearnerLearnerHE = new MessageLearnerLearnerHE();

            ApplyRule(s => s.UCASPERID, _standardRuleString.Definition, models, messageLearnerLearnerHE);
            ApplyRule(s => s.TTACCOM, _standardRuleLong.Definition, models, messageLearnerLearnerHE);

            return messageLearnerLearnerHE;
        }
    }
}
