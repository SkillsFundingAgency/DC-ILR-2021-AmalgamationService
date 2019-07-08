using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageSourceFile : AbstractLooseReadWriteModel<ILooseMessage>, ILooseSourceFile
    {
        public DateTime? DateTimeNullable
        {
            get => dateTimeFieldSpecified ? dateTimeField : default(DateTime?);
            set
            {
                dateTimeFieldSpecified = value.HasValue;
                dateTimeField = value.GetValueOrDefault();
            }
        }

        public string LearnRefNumber => null;
    }
}
