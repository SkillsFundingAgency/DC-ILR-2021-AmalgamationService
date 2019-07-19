using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LLDDandHealthProblemAmalgamator : AbstractAmalgamator<MessageLearnerLLDDandHealthProblem>, IAmalgamator<MessageLearnerLLDDandHealthProblem>
    {
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public LLDDandHealthProblemAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerEmploymentStatus, (x) => x.LearnRefNumber.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLLDDandHealthProblem Amalgamate(IEnumerable<MessageLearnerLLDDandHealthProblem> models)
        {
            var messageLearnerLLDDandHealthProblem = new MessageLearnerLLDDandHealthProblem();

            ApplyRule(s => s.LLDDCatNullable, _standardRuleLong.Definition, models, messageLearnerLLDDandHealthProblem);
            ApplyRule(s => s.PrimaryLLDDNullable, _standardRuleLong.Definition, models, messageLearnerLLDDandHealthProblem);

            return messageLearnerLLDDandHealthProblem;
        }
    }
}
