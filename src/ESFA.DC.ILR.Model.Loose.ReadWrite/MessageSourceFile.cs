using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageSourceFile : ILooseSourceFile
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

        public ILooseMessage Message { get; set; }

        public ILooseMessage Parent { get => Message; set => Message = value; }
        public string LearnRefNumber => null;
    }
}
