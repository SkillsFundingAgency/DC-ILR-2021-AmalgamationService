using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryAppFinRecord : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseAppFinRecord
    {
        [XmlIgnore]
        public long? AFinCodeNullable
        {
            get => aFinCodeFieldSpecified ? aFinCodeField : default(long?);
            set
            {
                aFinCodeFieldSpecified = value.HasValue;
                aFinCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? AFinDateNullable
        {
            get => aFinDateFieldSpecified ? aFinDateField : default(DateTime?);
            set
            {
                aFinDateFieldSpecified = value.HasValue;
                aFinDateField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? AFinAmountNullable
        {
            get => aFinAmountFieldSpecified ? aFinAmountField : default(long?);
            set
            {
                aFinAmountFieldSpecified = value.HasValue;
                aFinAmountField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
