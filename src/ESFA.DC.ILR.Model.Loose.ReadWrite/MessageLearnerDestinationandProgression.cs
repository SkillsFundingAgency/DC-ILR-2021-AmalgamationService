using System.Collections.Generic;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerDestinationandProgression : AbstractLooseReadWriteModel<Message> , IParentRelationship<Message>, IAmalgamationModel
    {
        [XmlIgnore]
        public string SourceFileName { get => Parent.Parent.Filename; }
    }
}
