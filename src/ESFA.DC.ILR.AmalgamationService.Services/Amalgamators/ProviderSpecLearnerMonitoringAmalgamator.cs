using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class ProviderSpecLearnerMonitoringAmalgamator : AbstractAmalgamator<MessageLearnerProviderSpecLearnerMonitoring>, IAmalgamator<MessageLearnerProviderSpecLearnerMonitoring>
    {
        private IRule<string> _standardRuleString;
        private IRule<string> _standardRuleFirstString;

        public ProviderSpecLearnerMonitoringAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.ProviderSpecLearnerMonitoring, (x) => x.ProvSpecLearnMonOccur.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleFirstString = ruleProvider.BuildStandardRuleFirstItem<string>();
        }

        public MessageLearnerProviderSpecLearnerMonitoring Amalgamate(IEnumerable<MessageLearnerProviderSpecLearnerMonitoring> models)
        {
            var messageLearnerProviderSpecLearnerMonitoring = new MessageLearnerProviderSpecLearnerMonitoring();

            ApplyRule(s => s.ProvSpecLearnMonOccur, _standardRuleString.Definition, models, messageLearnerProviderSpecLearnerMonitoring, Severity.Warning);
            ApplyRule(s => s.ProvSpecLearnMon, _standardRuleFirstString.Definition, models, messageLearnerProviderSpecLearnerMonitoring, Severity.Warning);

            return messageLearnerProviderSpecLearnerMonitoring;
        }
    }
}
