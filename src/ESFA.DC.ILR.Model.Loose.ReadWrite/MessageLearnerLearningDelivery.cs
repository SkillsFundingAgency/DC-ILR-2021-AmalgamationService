using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDelivery : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearningDelivery
    {
        public long? AimTypeNullable
        {
            get => aimTypeFieldSpecified ? aimTypeField : default(long?);
            set
            {
                aimTypeFieldSpecified = value.HasValue;
                aimTypeField = value.GetValueOrDefault();
            }
        }

        public long? AimSeqNumberNullable
        {
            get => aimSeqNumberFieldSpecified ? aimSeqNumberField : default(long?);
            set
            {
                aimSeqNumberFieldSpecified = value.HasValue;
                aimSeqNumberField = value.GetValueOrDefault();
            }
        }

        public long? FundModelNullable
        {
            get => fundModelFieldSpecified ? fundModelField : default(long?);
            set
            {
                fundModelFieldSpecified = value.HasValue;
                fundModelField = value.GetValueOrDefault();
            }
        }

        public long? ProgTypeNullable
        {
            get => progTypeFieldSpecified ? progTypeField : default(long?);
            set
            {
                progTypeFieldSpecified = value.HasValue;
                progTypeField = value.GetValueOrDefault();
            }
        }

        public long? FworkCodeNullable
        {
            get => fworkCodeFieldSpecified ? fworkCodeField : default(long?);
            set
            {
                fworkCodeFieldSpecified = value.HasValue;
                fworkCodeField = value.GetValueOrDefault();
            }
        }

        public long? PHoursNullable
        {
            get => pHoursFieldSpecified ? pHoursField : default(long?);
            set
            {
                pHoursFieldSpecified = value.HasValue;
                pHoursField = value.GetValueOrDefault();
            }
        }

        public long? PwayCodeNullable
        {
            get => pwayCodeFieldSpecified ? pwayCodeField : default(long?);
            set
            {
                pwayCodeFieldSpecified = value.HasValue;
                pwayCodeField = value.GetValueOrDefault();
            }
        }

        public long? StdCodeNullable
        {
            get => stdCodeFieldSpecified ? stdCodeField : default(long?);
            set
            {
                stdCodeFieldSpecified = value.HasValue;
                stdCodeField = value.GetValueOrDefault();
            }
        }

        public long? CompStatusNullable
        {
            get => compStatusFieldSpecified ? compStatusField : default(long?);
            set
            {
                compStatusFieldSpecified = value.HasValue;
                compStatusField = value.GetValueOrDefault();
            }
        }

        public DateTime? OrigLearnStartDateNullable
        {
            get => OrigLearnStartDateSpecified ? OrigLearnStartDate : default(DateTime?);
            set
            {
                OrigLearnStartDateSpecified = value.HasValue;
                OrigLearnStartDate = value.GetValueOrDefault();
            }
        }

        public DateTime? LearnStartDateNullable
        {
            get => learnStartDateFieldSpecified ? learnStartDateField : default(DateTime?);
            set
            {
                learnStartDateFieldSpecified = value.HasValue;
                learnStartDateField = value.GetValueOrDefault();
            }
        }

        public DateTime? LearnPlanEndDateNullable
        {
            get => learnPlanEndDateFieldSpecified ? learnPlanEndDateField : default(DateTime?);
            set
            {
                learnPlanEndDateFieldSpecified = value.HasValue;
                learnPlanEndDateField = value.GetValueOrDefault();
            }
        }

        public DateTime? LearnActEndDateNullable
        {
            get => LearnActEndDateSpecified ? LearnActEndDate : default(DateTime?);
            set
            {
                LearnActEndDateSpecified = value.HasValue;
                LearnActEndDate = value.GetValueOrDefault();
            }
        }

        public DateTime? AchDateNullable
        {
            get => AchDateSpecified ? AchDate : default(DateTime?);
            set
            {
                AchDateSpecified = value.HasValue;
                AchDate = value.GetValueOrDefault();
            }
        }

        public long? PartnerUKPRNNullable
        {
            get => partnerUKPRNFieldSpecified ? partnerUKPRNField : default(long?);
            set
            {
                partnerUKPRNFieldSpecified = value.HasValue;
                partnerUKPRNField = value.GetValueOrDefault();
            }
        }

        public long? OutcomeNullable
        {
            get => outcomeFieldSpecified ? outcomeField : default(long?);
            set
            {
                outcomeFieldSpecified = value.HasValue;
                outcomeField = value.GetValueOrDefault();
            }
        }

        public long? AddHoursNullable
        {
            get => addHoursFieldSpecified ? addHoursField : default(long?);
            set
            {
                addHoursFieldSpecified = value.HasValue;
                addHoursField = value.GetValueOrDefault();
            }
        }

        public long? PriorLearnFundAdjNullable
        {
            get => priorLearnFundAdjFieldSpecified ? priorLearnFundAdjField : default(long?);
            set
            {
                priorLearnFundAdjFieldSpecified = value.HasValue;
                priorLearnFundAdjField = value.GetValueOrDefault();
            }
        }

        public long? OtherFundAdjNullable
        {
            get => otherFundAdjFieldSpecified ? otherFundAdjField : default(long?);
            set
            {
                otherFundAdjFieldSpecified = value.HasValue;
                otherFundAdjField = value.GetValueOrDefault();
            }
        }

        public long? EmpOutcomeNullable
        {
            get => empOutcomeFieldSpecified ? empOutcomeField : default(long?);
            set
            {
                empOutcomeFieldSpecified = value.HasValue;
                empOutcomeField = value.GetValueOrDefault();
            }
        }

        public long? WithdrawReasonNullable
        {
            get => withdrawReasonFieldSpecified ? withdrawReasonField : default(long?);
            set
            {
                withdrawReasonFieldSpecified = value.HasValue;
                withdrawReasonField = value.GetValueOrDefault();
            }
        }

        public IReadOnlyCollection<ILooseLearningDeliveryFAM> LearningDeliveryFAMs
        {
            get => learningDeliveryFAMField;
            set => learningDeliveryFAMField = (MessageLearnerLearningDeliveryLearningDeliveryFAM[]) value;
        }

        public IReadOnlyCollection<ILooseAppFinRecord> AppFinRecords
        {
            get => appFinRecordField;
            set => appFinRecordField = (MessageLearnerLearningDeliveryAppFinRecord[]) value;
        }

        public IReadOnlyCollection<ILooseProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitorings
        {
            get => providerSpecDeliveryMonitoringField;
            set => providerSpecDeliveryMonitoringField = (MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring[]) value;
        }

        public IReadOnlyCollection<ILooseLearningDeliveryHE> LearningDeliveryHEs
        {
            get => learningDeliveryHEField;
            set => learningDeliveryHEField = (MessageLearnerLearningDeliveryLearningDeliveryHE[]) value;
        }

        public IReadOnlyCollection<ILooseLearningDeliveryWorkPlacement> LearningDeliveryWorkPlacements
        {
            get => learningDeliveryWorkPlacementField;
            set => learningDeliveryWorkPlacementField = (MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement[]) value;
        }

        public string SourceFileName => Parent.Parent.Parent.Filename;

        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
