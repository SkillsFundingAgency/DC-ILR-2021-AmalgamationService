using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract
{
    public abstract class AbstractLooseReadWriteModel<T> : IParentRelationshipSetter, IParentRelationship<T>
    {
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public T Parent { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public object ParentSetter { set => Parent = (T)value; }


    }
}
