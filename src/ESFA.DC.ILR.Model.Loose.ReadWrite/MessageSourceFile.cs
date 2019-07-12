using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageSourceFile : AbstractLooseReadWriteModel<ILooseMessage>, ILooseSourceFile
    {
        [XmlIgnore]
        public DateTime? DateTimeNullable
        {
            get => dateTimeFieldSpecified ? dateTimeField : default(DateTime?);
            set
            {
                dateTimeFieldSpecified = value.HasValue;
                dateTimeField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
