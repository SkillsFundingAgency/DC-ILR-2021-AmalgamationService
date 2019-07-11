using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLLDDandHealthProblem : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLLDDAndHealthProblem
    {
        [XmlIgnore]
        public long? LLDDCatNullable
        {
            get => lLDDCatFieldSpecified ? lLDDCatField : default(long?);
            set
            {
                lLDDCatFieldSpecified = value.HasValue;
                lLDDCatField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? PrimaryLLDDNullable
        {
            get => primaryLLDDFieldSpecified ? primaryLLDDField : default(long?);
            set
            {
                primaryLLDDFieldSpecified = value.HasValue;
                primaryLLDDField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
