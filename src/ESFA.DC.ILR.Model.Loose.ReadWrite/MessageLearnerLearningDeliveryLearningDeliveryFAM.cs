using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryLearningDeliveryFAM : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseLearningDeliveryFAM
    {
        [XmlIgnore]
        public DateTime? LearnDelFAMDateFromNullable
        {
            get => LearnDelFAMDateFromSpecified ? LearnDelFAMDateFrom : default(DateTime?);
            set
            {
                LearnDelFAMDateFromSpecified = value.HasValue;
                LearnDelFAMDateFrom = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public DateTime? LearnDelFAMDateToNullable
        {
            get => LearnDelFAMDateToSpecified ? LearnDelFAMDateTo : default(DateTime?);
            set
            {
                LearnDelFAMDateToSpecified = value.HasValue;
                LearnDelFAMDateTo = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
