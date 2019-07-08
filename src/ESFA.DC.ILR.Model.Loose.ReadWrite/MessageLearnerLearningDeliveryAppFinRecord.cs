using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryAppFinRecord : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseAppFinRecord
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

        public string SourceFileName => Parent.Parent.Parent.AmalgamationRoot.Filename;

        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
