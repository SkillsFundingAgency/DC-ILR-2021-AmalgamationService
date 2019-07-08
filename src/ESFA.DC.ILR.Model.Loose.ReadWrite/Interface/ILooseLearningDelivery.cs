using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearningDelivery : IParentRelationship<ILooseLearner>, IAmalgamationModel
    {
        string LearnAimRef { get;  set; }

        string DelLocPostCode { get;  set; }

        string ConRefNumber { get;  set; }

        string EPAOrgID { get;  set; }

        string OutGrade { get;  set; }

        string LSDPostcode { get;  set; }

        long? OutcomeNullable { get;  set; }

        string SWSupAimId { get;  set; }

        long? AimTypeNullable { get;  set; }

        long? AimSeqNumberNullable { get;  set; }

        long? FundModelNullable { get;  set; }

        long? ProgTypeNullable { get;  set; }

        long? FworkCodeNullable { get;  set; }

        long? PHoursNullable { get;  set; }

        long? PwayCodeNullable { get;  set; }

        long? StdCodeNullable { get;  set; }

        long? CompStatusNullable { get;  set; }
        
        DateTime? OrigLearnStartDateNullable { get;  set; }

        DateTime? LearnStartDateNullable { get;  set; }

        DateTime? LearnPlanEndDateNullable { get;  set; }

        DateTime? LearnActEndDateNullable { get;  set; }

        DateTime? AchDateNullable { get;  set; }

        long? PartnerUKPRNNullable { get;  set; }

        long? AddHoursNullable { get;  set; }

        long? PriorLearnFundAdjNullable { get;  set; }

        long? OtherFundAdjNullable { get;  set; }

        long? EmpOutcomeNullable { get;  set; }

        long? WithdrawReasonNullable { get;  set; }

        IReadOnlyCollection<ILooseLearningDeliveryFAM> LearningDeliveryFAMs { get;  set; }

        IReadOnlyCollection<ILooseAppFinRecord> AppFinRecords { get;  set; }

        IReadOnlyCollection<ILooseProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitorings { get;  set; }

        IReadOnlyCollection<ILooseLearningDeliveryHE> LearningDeliveryHEs { get;  set; }

        IReadOnlyCollection<ILooseLearningDeliveryWorkPlacement> LearningDeliveryWorkPlacements { get;  set; }
    }
}
