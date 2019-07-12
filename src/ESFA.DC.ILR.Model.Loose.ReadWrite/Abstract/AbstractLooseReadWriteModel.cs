using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract
{
    public abstract class AbstractLooseReadWriteModel<T> : IParentRelationshipSetter, IParentRelationship<T>
    {
        [XmlIgnore]
        public T Parent { get; set; }

        [XmlIgnore]
        public object ParentSetter { set => Parent = (T)value; }


    }
}
