using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;
namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearner : AbstractLooseReadWriteModel<Message>, IParentRelationship<Message>, IAmalgamationModel
    {
       
        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Filename;
    }
}
