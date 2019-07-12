using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerEmploymentStatusAmalgamator : AbstractAmalgamator<MessageLearnerLearnerEmploymentStatus>, IAmalgamator<MessageLearnerLearnerEmploymentStatus>
    {
        private IRule<string> _standardRuleString;
        private IRule<long> _standardRuleLong;
        private IRule<DateTime> _standardRuleDateTime;

        public LearnerEmploymentStatusAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.LearnerEmploymentStatus, (x) => x.DateEmpStatApp.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long>();
            _standardRuleDateTime = ruleProvider.BuildStandardRule<DateTime>();
        }

        public MessageLearnerLearnerEmploymentStatus Amalgamate(IEnumerable<MessageLearnerLearnerEmploymentStatus> models)
        {
            var messageLearnerLearnerEmploymentStatus = new MessageLearnerLearnerEmploymentStatus();

            ApplyRule(s => s.DateEmpStatApp, _standardRuleDateTime.Definition, models, messageLearnerLearnerEmploymentStatus);
            ApplyRule(s => s.EmpStat, _standardRuleLong.Definition, models, messageLearnerLearnerEmploymentStatus);
            ApplyRule(s => s.EmpId, _standardRuleLong.Definition, models, messageLearnerLearnerEmploymentStatus);
            ApplyRule(s => s.AgreeId, _standardRuleString.Definition, models, messageLearnerLearnerEmploymentStatus);

            return messageLearnerLearnerEmploymentStatus;
        }
    }
}
