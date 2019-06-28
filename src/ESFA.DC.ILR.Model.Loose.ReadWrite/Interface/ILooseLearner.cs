using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearner : IParentRelationship<ILooseMessage>
    {
        string LearnRefNumber { get;  set; }

        string PrevLearnRefNumber { get;  set; }

        string FamilyName { get;  set; }

        string GivenNames { get;  set; }

        string Sex { get;  set; }

        string NINumber { get;  set; }

        string MathGrade { get;  set; }

        string EngGrade { get;  set; }

        string PostcodePrior { get;  set; }

        string Postcode { get;  set; }

        string AddLine1 { get;  set; }

        string AddLine2 { get;  set; }

        string AddLine3 { get;  set; }

        string AddLine4 { get;  set; }

        string TelNo { get;  set; }

        string Email { get;  set; }

        string CampId { get;  set; }

        long? ULNNullable { get;  set; }

        long? EthnicityNullable { get;  set; }

        long? LLDDHealthProbNullable { get;  set; }

        long? PrevUKPRNNullable { get;  set; }

        long? PMUKPRNNullable { get;  set; }

        long? PriorAttainNullable { get;  set; }

        long? AccomNullable { get;  set; }

        long? ALSCostNullable { get;  set; }

        long? PlanLearnHoursNullable { get;  set; }

        long? PlanEEPHoursNullable { get;  set; }

        DateTime? DateOfBirthNullable { get;  set; }

        IReadOnlyCollection<ILooseContactPreference> ContactPreferences { get;  set; }

        IReadOnlyCollection<ILooseLearnerFAM> LearnerFAMs { get;  set; }

        IReadOnlyCollection<ILooseProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings { get;  set; }

        IReadOnlyCollection<ILooseLearnerEmploymentStatus> LearnerEmploymentStatuses { get;  set; }

        IReadOnlyCollection<ILooseLearnerHE> LearnerHEs { get;  set; }

        IReadOnlyCollection<ILooseLearningDelivery> LearningDeliveries { get;  set; }

        IReadOnlyCollection<ILooseLLDDAndHealthProblem> LLDDAndHealthProblems { get;  set; }

        ILooseMessage Message { get; set; }
    }
}
