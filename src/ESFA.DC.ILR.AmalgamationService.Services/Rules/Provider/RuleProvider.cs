using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory
{
    public class RuleProvider : IRuleProvider
    {
        public IRule<T> BuildStandardRule<T>()
        {
            return new StandardRule<T>();
        }

        public IRule<T> BuildStandardRuleFirstItem<T>()
        {
            return new StandardRuleFirstItem<T>();
        }

        public IRule<MessageLearner> BuildAddressRule()
        {
            return new AddressRule();
        }

        public IRule<long?> BuildUlnRule()
        {
            return new UlnRule();
        }

        public IRule<long?> BuildAlsCostRule()
        {
            return new AlsCostRule();
        }

        public IRule<string> BuildPostCodeRule()
        {
            return new PostCodeRule();
        }

        public IRule<List<MessageLearnerContactPreference>> BuildLearnerContactPreferenceCollectionRule()
        {
            return new LearnerContactPreferenceCollectionRule();
        }

        public IRule<List<MessageLearnerLLDDandHealthProblem>> BuildLLDDandHealthProblemCollectionRule()
        {
            return new LLDDandHealthProblemCollectionRule();
        }

        public IRule<List<MessageLearnerLearnerFAM>> BuildLearnerFAMAmalgamationRule()
        {
            return new LearnerFAMAmalgamationRule();
        }

        public IRule<List<MessageLearnerLearningDelivery>> BuildLearningDeliveryRule()
        {
            return new LearningDeliveryRule();
        }

        public IRule<List<MessageLearnerDestinationandProgressionDPOutcome>> BuildDPOutcomeRule()
        {
            return new DPOutcomeRule();
        }
    }
}
