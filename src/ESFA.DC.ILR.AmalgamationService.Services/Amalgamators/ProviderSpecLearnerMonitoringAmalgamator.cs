using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators
{
    public class ProviderSpecLearnerMonitoringAmalgamator : AbstractAmalgamator<MessageLearnerProviderSpecLearnerMonitoring>, IAmalgamator<MessageLearnerProviderSpecLearnerMonitoring>
    {
        private IRule<string> _standardRuleString;
        private IRule<long?> _standardRuleLong;

        public ProviderSpecLearnerMonitoringAmalgamator(IRuleProvider ruleProvider, IAmalgamationErrorHandler amalgamationErrorHandler)
            : base(Entity.ProviderSpecLearnerMonitoring, (x) => x.ProvSpecLearnMonOccur.ToString(), amalgamationErrorHandler)
        {
            _standardRuleString = ruleProvider.BuildStandardRule<string>();
            _standardRuleLong = ruleProvider.BuildStandardRule<long?>();
        }

        public MessageLearnerProviderSpecLearnerMonitoring Amalgamate(IEnumerable<MessageLearnerProviderSpecLearnerMonitoring> models)
        {
            var messageLearnerProviderSpecLearnerMonitoring = new MessageLearnerProviderSpecLearnerMonitoring();

            // TODO : apply rules
            throw new NotImplementedException();
        }
    }
}
