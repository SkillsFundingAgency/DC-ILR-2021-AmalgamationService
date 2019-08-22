using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleProvider
    {
        IRule<T> BuildStandardRule<T>();

        IRule<MessageLearner> BuildAddressRule();

        IRule<long?> BuildUlnRule();

        IRule<long?> BuildAlsCostRule();

        IRule<string> BuildPostCodeRule();

        IRule<MessageLearnerContactPreference[]> BuildLearnerContactPreferenceCollectionRule();

        IRule<MessageLearnerLLDDandHealthProblem[]> BuildLLDDandHealthProblemCollectionRule();

        IRule<MessageLearnerLearnerFAM[]> BuildLearnerFAMAmalgamationRule();

        IRule<MessageLearnerLearningDelivery[]> BuildLearningDeliveryRule();

        IRule<MessageLearnerDestinationandProgressionDPOutcome[]> BuildDPOutcomeRule();
    }
}
