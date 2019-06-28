using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearner : ILooseLearner
    {
        public long? ULNNullable
        {
            get => uLNFieldSpecified ? uLNField : default(long?);
            set
            {
                uLNFieldSpecified = value.HasValue;
                uLNField = value.GetValueOrDefault();
            }
        }

        public long? EthnicityNullable
        {
            get => ethnicityFieldSpecified ? ethnicityField : default(long?);
            set
            {
                ethnicityFieldSpecified = value.HasValue;
                ethnicityField = value.GetValueOrDefault();
            }
        }

        public long? LLDDHealthProbNullable
        {
            get => lLDDHealthProbFieldSpecified ? lLDDHealthProbField : default(long?);
            set
            {
                lLDDHealthProbFieldSpecified = value.HasValue;
                lLDDHealthProbField = value.GetValueOrDefault();
            }
        }

        public long? PrevUKPRNNullable
        {
            get => prevUKPRNFieldSpecified ? prevUKPRNField : default(long?);
            set
            {
                prevUKPRNFieldSpecified = value.HasValue;
                prevUKPRNField = value.GetValueOrDefault();
            }
        }

        public long? PMUKPRNNullable
        {
            get => pMUKPRNFieldSpecified ? pMUKPRNField : default(long?);
            set
            {
                pMUKPRNFieldSpecified = value.HasValue;
                pMUKPRNField = value.GetValueOrDefault();
            }
        }

        public long? PriorAttainNullable
        {
            get => priorAttainFieldSpecified ? priorAttainField : default(long?);
            set
            {
                priorAttainFieldSpecified = value.HasValue;
                priorAttainField = value.GetValueOrDefault();
            }
        }

        public long? AccomNullable
        {
            get => accomFieldSpecified ? accomField : default(long?);
            set
            {
                accomFieldSpecified = value.HasValue;
                accomField = value.GetValueOrDefault();
            }
        }

        public long? ALSCostNullable
        {
            get => aLSCostFieldSpecified ? aLSCostField : default(long?);
            set
            {
                aLSCostFieldSpecified = value.HasValue;
                aLSCostField = value.GetValueOrDefault();
            }
        }

        public long? PlanLearnHoursNullable
        {
            get => planLearnHoursFieldSpecified ? planLearnHoursField : default(long?);
            set
            {
                planLearnHoursFieldSpecified = value.HasValue;
                planLearnHoursField = value.GetValueOrDefault();
            }
        }

        public long? PlanEEPHoursNullable
        {
            get => planEEPHoursFieldSpecified ? planEEPHoursField : default(long?);
            set
            {
                planEEPHoursFieldSpecified = value.HasValue;
                planEEPHoursField = value.GetValueOrDefault();
            }
        }

        public DateTime? DateOfBirthNullable
        {
            get => dateOfBirthFieldSpecified ? dateOfBirthField : default(DateTime?);
            set
            {
                dateOfBirthFieldSpecified = value.HasValue;
                dateOfBirthField = value.GetValueOrDefault();
            }
        }

        public IReadOnlyCollection<ILooseContactPreference> ContactPreferences
        {
            get => contactPreferenceField;
            set => contactPreferenceField = (MessageLearnerContactPreference[])value;
        }

        public IReadOnlyCollection<ILooseLearnerFAM> LearnerFAMs
        {
            get => learnerFAMField;
            set => learnerFAMField = (MessageLearnerLearnerFAM[])value;
        }

        public IReadOnlyCollection<ILooseProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings
        {
            get => providerSpecLearnerMonitoringField;
            set => providerSpecLearnerMonitoringField = (MessageLearnerProviderSpecLearnerMonitoring[])value;
        }

        public IReadOnlyCollection<ILooseLearnerEmploymentStatus> LearnerEmploymentStatuses
        {
            get => learnerEmploymentStatusField;
            set => learnerEmploymentStatusField = (MessageLearnerLearnerEmploymentStatus[])value;
        }

        public IReadOnlyCollection<ILooseLearnerHE> LearnerHEs
        {
            get => learnerHEField;
            set => learnerHEField = (MessageLearnerLearnerHE[])value;
        }

        public IReadOnlyCollection<ILooseLearningDelivery> LearningDeliveries
        {
            get => learningDeliveryField;
            set => learningDeliveryField = (MessageLearnerLearningDelivery[])value;
        }

        public IReadOnlyCollection<ILooseLLDDAndHealthProblem> LLDDAndHealthProblems
        {
            get => lLDDandHealthProblemField;
            set => lLDDandHealthProblemField = (MessageLearnerLLDDandHealthProblem[])value;
        }

        public ILooseMessage Message { get; set; }

        public ILooseMessage Parent { get => Message; set => Message = value; }
    }
}
