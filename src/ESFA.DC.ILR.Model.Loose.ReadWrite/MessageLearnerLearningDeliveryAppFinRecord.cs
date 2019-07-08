using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryAppFinRecord : ILooseAppFinRecord
    {
        public long? AFinCodeNullable
        {
            get => aFinCodeFieldSpecified ? aFinCodeField : default(long?);
            set
            {
                aFinCodeFieldSpecified = value.HasValue;
                aFinCodeField = value.GetValueOrDefault();
            }
        }

        public DateTime? AFinDateNullable
        {
            get => aFinDateFieldSpecified ? aFinDateField : default(DateTime?);
            set
            {
                aFinDateFieldSpecified = value.HasValue;
                aFinDateField = value.GetValueOrDefault();
            }
        }

        public long? AFinAmountNullable
        {
            get => aFinAmountFieldSpecified ? aFinAmountField : default(long?);
            set
            {
                aFinAmountFieldSpecified = value.HasValue;
                aFinAmountField = value.GetValueOrDefault();
            }
        }

        public ILooseLearningDelivery LearningDelivery { get; set; }
        public ILooseLearningDelivery Parent { get => LearningDelivery; set => LearningDelivery = value; }

        public string SourceFileName => LearningDelivery.SourceFileName;

        public string LearnRefNumber => LearningDelivery.LearnRefNumber;
    }
}
