using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;
namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearner : AbstractLooseReadWriteModel<ILooseMessage>, ILooseLearner
    {
        [XmlIgnore]
        public long? ULNNullable
        {
            get => uLNFieldSpecified ? uLNField : default(long?);
            set
            {
                uLNFieldSpecified = value.HasValue;
                uLNField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? EthnicityNullable
        {
            get => ethnicityFieldSpecified ? ethnicityField : default(long?);
            set
            {
                ethnicityFieldSpecified = value.HasValue;
                ethnicityField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? LLDDHealthProbNullable
        {
            get => lLDDHealthProbFieldSpecified ? lLDDHealthProbField : default(long?);
            set
            {
                lLDDHealthProbFieldSpecified = value.HasValue;
                lLDDHealthProbField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PrevUKPRNNullable
        {
            get => prevUKPRNFieldSpecified ? prevUKPRNField : default(long?);
            set
            {
                prevUKPRNFieldSpecified = value.HasValue;
                prevUKPRNField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PMUKPRNNullable
        {
            get => pMUKPRNFieldSpecified ? pMUKPRNField : default(long?);
            set
            {
                pMUKPRNFieldSpecified = value.HasValue;
                pMUKPRNField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PriorAttainNullable
        {
            get => priorAttainFieldSpecified ? priorAttainField : default(long?);
            set
            {
                priorAttainFieldSpecified = value.HasValue;
                priorAttainField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? AccomNullable
        {
            get => accomFieldSpecified ? accomField : default(long?);
            set
            {
                accomFieldSpecified = value.HasValue;
                accomField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? ALSCostNullable
        {
            get => aLSCostFieldSpecified ? aLSCostField : default(long?);
            set
            {
                aLSCostFieldSpecified = value.HasValue;
                aLSCostField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PlanLearnHoursNullable
        {
            get => planLearnHoursFieldSpecified ? planLearnHoursField : default(long?);
            set
            {
                planLearnHoursFieldSpecified = value.HasValue;
                planLearnHoursField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PlanEEPHoursNullable
        {
            get => planEEPHoursFieldSpecified ? planEEPHoursField : default(long?);
            set
            {
                planEEPHoursFieldSpecified = value.HasValue;
                planEEPHoursField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? DateOfBirthNullable
        {
            get => dateOfBirthFieldSpecified ? dateOfBirthField : default(DateTime?);
            set
            {
                dateOfBirthFieldSpecified = value.HasValue;
                dateOfBirthField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseContactPreference> ContactPreferences
        {
            get => contactPreferenceField;
            set => contactPreferenceField = (MessageLearnerContactPreference[])value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearnerFAM> LearnerFAMs
        {
            get => learnerFAMField;
            set => learnerFAMField = (MessageLearnerLearnerFAM[])value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings
        {
            get => providerSpecLearnerMonitoringField;
            set => providerSpecLearnerMonitoringField = (MessageLearnerProviderSpecLearnerMonitoring[])value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearnerEmploymentStatus> LearnerEmploymentStatuses
        {
            get => learnerEmploymentStatusField;
            set => learnerEmploymentStatusField = (MessageLearnerLearnerEmploymentStatus[])value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearnerHE> LearnerHEs
        {
            get => learnerHEField;
            set => learnerHEField = (MessageLearnerLearnerHE[])value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearningDelivery> LearningDeliveries
        {
            get => learningDeliveryField;
            set => learningDeliveryField = (MessageLearnerLearningDelivery[])value;
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLLDDAndHealthProblem> LLDDAndHealthProblems
        {
            get => lLDDandHealthProblemField;
            set => lLDDandHealthProblemField = (MessageLearnerLLDDandHealthProblem[])value;
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Filename;
    }
}
