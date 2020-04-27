using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerHELearnerHEFinancialSupport : AbstractLooseReadWriteModel<ILooseLearnerHE>, ILooseLearnerHEFinancialSupport
    {
        [XmlIgnore]
        public long? FINTYPENullable
        {
            get => fINTYPEFieldSpecified ? fINTYPEField : default(long?);
            set
            {
                fINTYPEFieldSpecified = value.HasValue;
                fINTYPEField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? FINAMOUNTNullable
        {
            get => fINAMOUNTFieldSpecified ? fINAMOUNTField : default(long?);
            set
            {
                fINAMOUNTFieldSpecified = value.HasValue;
                fINAMOUNTField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
