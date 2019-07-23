using ESFA.DC.ILR.AmalgamationService.Interfaces.Attribute;
using System;
using System.ComponentModel;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces.Enum
{
    public enum Entity
    {
        [KeyProperty("LearnRefNumber")]
        [Description("Message")]
        Message,

        [KeyProperty("LearnRefNumber")]
        [Description("Header")]
        Header,

        [KeyProperty("LearnRefNumber")]
        [Description("CollectionDetails")]
        CollectionDetails,

        [KeyProperty("LearnRefNumber")]
        [Description("Source")]
        Source,

        [KeyProperty("LearnRefNumber")]
        [Description("SourceFiles")]
        SourceFiles,

        [KeyProperty("LearnRefNumber")]
        [Description("Learning Provider")]
        LearningProvider,

        [KeyProperty("LearnRefNumber")]
        [Description("Learner")]
        Learner,

        [KeyProperty("LearnRefNumber")]
        [Description("Learner Destination and Progression")]
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

        [KeyProperty("LearnRefNumber")]
        [Description("Learning Delivery")]
        LearningDelivery,

        [KeyProperty("FINTYPE")]
        [Description("Learner HE Financial Support")]
        LearnerHEFinancialSupport,

        [KeyProperty("ESMType")]
        [Description("Learner Employment Status Monitoring")]
        EmploymentStatusMonitoring,

        [KeyProperty("OutStartDate")]
        [Description("Learner Destination and Progression DPOutcome")]
        LearnerDestinationandProgressionDPOutcome,

        [KeyProperty("LLDDCat")]
        [Description("Learner LLDD and Health problem")]
        LLDDAndHealthProblems
    }
}
