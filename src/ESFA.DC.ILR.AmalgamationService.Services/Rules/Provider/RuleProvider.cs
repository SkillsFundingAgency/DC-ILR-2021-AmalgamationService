using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory
{
    public class RuleProvider : IRuleProvider
    {
        public IRule<T> BuildStandardRule<T>()
        {
            return new StandardRule<T>();
        }

        public IRule<string> BuildAddressRule()
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

        public IRule<MessageLearnerContactPreference[]> BuildLearnerContactPreferenceCollectionRule()
        {
            return new LearnerContactPreferenceCollectionRule();
        }

        public IRule<MessageLearnerLLDDandHealthProblem[]> BuildLLDDandHealthProblemCollectionRule()
        {
            return new LLDDandHealthProblemCollectionRule();
        }

        public IRule<MessageLearnerLearnerFAM[]> BuildLearnerFAMAmalgamationRule()
        {
            return new LearnerFAMAmalgamationRule();
        }

        public IRule<MessageLearnerLearningDelivery[]> BuildLearningDeliveryRule()
        {
            return new LearningDeliveryRule();
        }
    }
}
