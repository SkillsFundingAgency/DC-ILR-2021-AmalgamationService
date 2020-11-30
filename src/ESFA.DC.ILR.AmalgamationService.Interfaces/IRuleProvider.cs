using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleProvider
    {
        IRule<T> BuildStandardRule<T>();

        IRule<T> BuildStandardRuleFirstItem<T>();

        IRule<MessageLearner> BuildAddressRule();

        IRule<long?> BuildUlnRule();

        IRule<long?> BuildAlsCostRule();

        IRule<string> BuildPostCodeRule();

        IRule<List<MessageLearnerContactPreference>> BuildLearnerContactPreferenceCollectionRule();

        IRule<List<MessageLearnerLLDDandHealthProblem>> BuildLLDDandHealthProblemCollectionRule();

        IRule<List<MessageLearnerLearnerFAM>> BuildLearnerFAMAmalgamationRule();

        IRule<List<MessageLearnerLearningDelivery>> BuildLearningDeliveryRule();

        IRule<List<MessageLearnerDestinationandProgressionDPOutcome>> BuildDPOutcomeRule();
    }
}
