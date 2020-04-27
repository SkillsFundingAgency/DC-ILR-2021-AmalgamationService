using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDelivery : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearningDelivery
    {
        [XmlIgnore]
        public long? AimTypeNullable
        {
            get => aimTypeFieldSpecified ? aimTypeField : default(long?);
            set
            {
                aimTypeFieldSpecified = value.HasValue;
                aimTypeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? AimSeqNumberNullable
        {
            get => aimSeqNumberFieldSpecified ? aimSeqNumberField : default(long?);
            set
            {
                aimSeqNumberFieldSpecified = value.HasValue;
                aimSeqNumberField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? FundModelNullable
        {
            get => fundModelFieldSpecified ? fundModelField : default(long?);
            set
            {
                fundModelFieldSpecified = value.HasValue;
                fundModelField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? ProgTypeNullable
        {
            get => progTypeFieldSpecified ? progTypeField : default(long?);
            set
            {
                progTypeFieldSpecified = value.HasValue;
                progTypeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? FworkCodeNullable
        {
            get => fworkCodeFieldSpecified ? fworkCodeField : default(long?);
            set
            {
                fworkCodeFieldSpecified = value.HasValue;
                fworkCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PHoursNullable
        {
            get => pHoursFieldSpecified ? pHoursField : default(long?);
            set
            {
                pHoursFieldSpecified = value.HasValue;
                pHoursField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PwayCodeNullable
        {
            get => pwayCodeFieldSpecified ? pwayCodeField : default(long?);
            set
            {
                pwayCodeFieldSpecified = value.HasValue;
                pwayCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? StdCodeNullable
        {
            get => stdCodeFieldSpecified ? stdCodeField : default(long?);
            set
            {
                stdCodeFieldSpecified = value.HasValue;
                stdCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? CompStatusNullable
        {
            get => compStatusFieldSpecified ? compStatusField : default(long?);
            set
            {
                compStatusFieldSpecified = value.HasValue;
                compStatusField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? OrigLearnStartDateNullable
        {
            get => OrigLearnStartDateSpecified ? OrigLearnStartDate : default(DateTime?);
            set
            {
                OrigLearnStartDateSpecified = value.HasValue;
                OrigLearnStartDate = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? LearnStartDateNullable
        {
            get => learnStartDateFieldSpecified ? learnStartDateField : default(DateTime?);
            set
            {
                learnStartDateFieldSpecified = value.HasValue;
                learnStartDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? LearnPlanEndDateNullable
        {
            get => learnPlanEndDateFieldSpecified ? learnPlanEndDateField : default(DateTime?);
            set
            {
                learnPlanEndDateFieldSpecified = value.HasValue;
                learnPlanEndDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? LearnActEndDateNullable
        {
            get => LearnActEndDateSpecified ? LearnActEndDate : default(DateTime?);
            set
            {
                LearnActEndDateSpecified = value.HasValue;
                LearnActEndDate = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? AchDateNullable
        {
            get => AchDateSpecified ? AchDate : default(DateTime?);
            set
            {
                AchDateSpecified = value.HasValue;
                AchDate = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PartnerUKPRNNullable
        {
            get => partnerUKPRNFieldSpecified ? partnerUKPRNField : default(long?);
            set
            {
                partnerUKPRNFieldSpecified = value.HasValue;
                partnerUKPRNField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? OutcomeNullable
        {
            get => outcomeFieldSpecified ? outcomeField : default(long?);
            set
            {
                outcomeFieldSpecified = value.HasValue;
                outcomeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? AddHoursNullable
        {
            get => addHoursFieldSpecified ? addHoursField : default(long?);
            set
            {
                addHoursFieldSpecified = value.HasValue;
                addHoursField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PriorLearnFundAdjNullable
        {
            get => priorLearnFundAdjFieldSpecified ? priorLearnFundAdjField : default(long?);
            set
            {
                priorLearnFundAdjFieldSpecified = value.HasValue;
                priorLearnFundAdjField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? OtherFundAdjNullable
        {
            get => otherFundAdjFieldSpecified ? otherFundAdjField : default(long?);
            set
            {
                otherFundAdjFieldSpecified = value.HasValue;
                otherFundAdjField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? EmpOutcomeNullable
        {
            get => empOutcomeFieldSpecified ? empOutcomeField : default(long?);
            set
            {
                empOutcomeFieldSpecified = value.HasValue;
                empOutcomeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? WithdrawReasonNullable
        {
            get => withdrawReasonFieldSpecified ? withdrawReasonField : default(long?);
            set
            {
                withdrawReasonFieldSpecified = value.HasValue;
                withdrawReasonField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearningDeliveryFAM> LearningDeliveryFAMs
        {
            get => learningDeliveryFAMField;
            set => learningDeliveryFAMField = (MessageLearnerLearningDeliveryLearningDeliveryFAM[]) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseAppFinRecord> AppFinRecords
        {
            get => appFinRecordField;
            set => appFinRecordField = (MessageLearnerLearningDeliveryAppFinRecord[]) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseProviderSpecDeliveryMonitoring> ProviderSpecDeliveryMonitorings
        {
            get => providerSpecDeliveryMonitoringField;
            set => providerSpecDeliveryMonitoringField = (MessageLearnerLearningDeliveryProviderSpecDeliveryMonitoring[]) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearningDeliveryHE> LearningDeliveryHEs
        {
            get => learningDeliveryHEField;
            set => learningDeliveryHEField = (MessageLearnerLearningDeliveryLearningDeliveryHE[]) value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearningDeliveryWorkPlacement> LearningDeliveryWorkPlacements
        {
            get => learningDeliveryWorkPlacementField;
            set => learningDeliveryWorkPlacementField = (MessageLearnerLearningDeliveryLearningDeliveryWorkPlacement[]) value;
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
