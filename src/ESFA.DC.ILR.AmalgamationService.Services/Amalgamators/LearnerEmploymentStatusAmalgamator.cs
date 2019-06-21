using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerEmploymentStatusAmalgamator : AbstractAmalgamator, IAmalgamator<MessageLearnerLearnerEmploymentStatus>
    {
        private IRule<string> _standardRuleString;
        private IRule<long> _standardRuleLong;

        public LearnerEmploymentStatusAmalgamator(IRuleProvider ruleProvider)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long>();
        }

        public MessageLearnerLearnerEmploymentStatus Amalgamate(IEnumerable<MessageLearnerLearnerEmploymentStatus> models)
        {
            var messageLearnerLearnerEmploymentStatus = new MessageLearnerLearnerEmploymentStatus() { DateEmpStatApp = models.FirstOrDefault().DateEmpStatApp };

            ApplyRule(s => s.EmpStat, _standardRuleLong.Definition, models, messageLearnerLearnerEmploymentStatus);
            ApplyRule(s => s.EmpId, _standardRuleLong.Definition, models, messageLearnerLearnerEmploymentStatus);
            ApplyRule(s => s.AgreeId, _standardRuleString.Definition, models, messageLearnerLearnerEmploymentStatus);

            return messageLearnerLearnerEmploymentStatus;
        }
    }
}
