using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleProvider
    {
        IRule<T> BuildStandardRule<T>();

        IRule<string> BuildAddressRule();

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
