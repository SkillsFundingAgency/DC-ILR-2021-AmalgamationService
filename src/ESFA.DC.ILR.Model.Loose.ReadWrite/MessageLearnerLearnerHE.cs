using System.Collections.Generic;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerHE : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearnerHE
    {
        [XmlIgnore]
        public long? TTACCOMNullable
        {
            get => tTACCOMFieldSpecified ? tTACCOMField : default(long?);
            set
            {
                tTACCOMFieldSpecified = value.HasValue;
                tTACCOMField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseLearnerHEFinancialSupport> LearnerHEFinancialSupports
        {
            get => learnerHEFinancialSupportField;
            set => learnerHEFinancialSupportField = (MessageLearnerLearnerHELearnerHEFinancialSupport[]) value;
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.Parent.LearnRefNumber;
    }
}
