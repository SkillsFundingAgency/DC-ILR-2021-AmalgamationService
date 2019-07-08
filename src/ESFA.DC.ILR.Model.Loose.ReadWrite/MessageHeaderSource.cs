using System;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Extension;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeaderSource : AbstractLooseReadWriteModel<ILooseHeader>, ILooseSource
    {
        public string ProtectiveMarkingString
        {
            get => protectiveMarkingField.XmlEnumToString();
            set => protectiveMarkingField = (MessageHeaderSourceProtectiveMarking)Enum.Parse(typeof(MessageHeaderSourceProtectiveMarking), value);
        }

        public string SourceFileName { get => Parent.Parent.Parent.Filename; }
        public string LearnRefNumber => null;
    }
}
