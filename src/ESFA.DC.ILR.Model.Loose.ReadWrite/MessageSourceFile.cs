using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageSourceFile : AbstractLooseReadWriteModel<Message>, IParentRelationship<Message>, IAmalgamationModel
    {
       
        [XmlIgnore]
        public string LearnRefNumber => null;

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Filename;
    }
}
