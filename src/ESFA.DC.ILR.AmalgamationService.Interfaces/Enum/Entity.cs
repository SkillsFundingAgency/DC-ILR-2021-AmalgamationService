using ESFA.DC.ILR.AmalgamationService.Interfaces.Attribute;
using System;
using System.ComponentModel;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces.Enum
{
    public enum Entity
    {
        Message,
        Header,
        CollectionDetails,
        Source,
        CollectionDetailsEntity,
        SourceEntity,

        SourceFiles,
        LearningProvider,

        [KeyProperty("LearnRefNumber")]
        [Description("Learner")]
        Learner,

        LearnerDestinationandProgression,

        [KeyProperty("ContPrefType")]
        [Description("Learner Contact Preference")]
        ContactPreference,

        [KeyProperty("LLDDCat")]
        [Description("Learner LLDD and Health problem")]
        LLDDandHealthProblem,

        [KeyProperty("LearnFAMType")]
        [Description("Learner Funding and Monitoring")]
        LearnerFAM,

        [KeyProperty("ProvSpecLearnMonOccur")]
        [Description("Learner Provider Specified Monitoring")]
        ProviderSpecLearnerMonitoring,

        [KeyProperty("DateEmpStatApp")]
        [Description("Learner Employment Status")]
        LearnerEmploymentStatus,

        [KeyProperty("LearnRefNumber")]
        [Description("Learner HE")]
        LearnerHE,

        LearningDelivery,
        ContactPreferences,
        LearnerFAMs,
        ProviderSpecLearnerMonitorings,
        LearnerEmploymentStatuses,
        LearnerHEs,
        LearningDeliveries,
        LLDDAndHealthProblems
    }
}
