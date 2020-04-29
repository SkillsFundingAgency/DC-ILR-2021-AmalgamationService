using System.Collections.Generic;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class Message : AbstractLooseReadWriteModel<IAmalgamationRoot>, IParentRelationship<IAmalgamationRoot>, IAmalgamationModel
    {
        [XmlIgnore]
        public IAmalgamationRoot AmalgamationRoot { get; set; }

        [XmlIgnore]
        public string SourceFileName => Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
