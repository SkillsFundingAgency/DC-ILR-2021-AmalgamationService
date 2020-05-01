using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class LearnerEmploymentStatusMonitoringAmalgamator : AbstractAmalgamator<MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring>, IAmalgamator<MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring>
    {
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public LearnerEmploymentStatusMonitoringAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.EmploymentStatusMonitoring, (x) => x.ESMType.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring Amalgamate(IEnumerable<MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring> models)
        {
            var learnerEmploymentStatusEmploymentStatusMonitoring = new MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring();

            ApplyRule(s => s.ESMType, _standardRuleString.Definition, models, learnerEmploymentStatusEmploymentStatusMonitoring);
            ApplyRule(s => s.ESMCode, _standardRuleLong.Definition, models, learnerEmploymentStatusEmploymentStatusMonitoring);

            return learnerEmploymentStatusEmploymentStatusMonitoring;
        }
    }
}
