using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerContactPreference : AbstractLooseReadWriteModel<ILooseLearner>,  ILooseContactPreference
    {

        [XmlIgnore]
        public long? ContPrefCodeNullable
        {
            get => contPrefCodeFieldSpecified ? contPrefCodeField : default(long?);
            set
            {
                contPrefCodeFieldSpecified = value.HasValue;
                contPrefCodeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
