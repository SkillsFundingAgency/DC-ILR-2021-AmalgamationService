using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract
{
    public abstract class AbstractLooseReadWriteModel<T> : IParentRelationship<T>, IParentRelationshipSetter
    {
        public T Parent { get; set; }

        public object ParentSetter { set => Parent = (T)value; }
    }
}
