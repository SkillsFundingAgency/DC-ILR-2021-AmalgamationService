using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageSourceFilesSourceFile : AbstractLooseReadWriteModel<Message>, IParentRelationship<Message>, IAmalgamationModel
    {
        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
