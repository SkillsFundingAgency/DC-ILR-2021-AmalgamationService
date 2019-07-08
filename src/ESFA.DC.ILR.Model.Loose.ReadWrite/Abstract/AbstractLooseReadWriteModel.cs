using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract
{
    public abstract class AbstractLooseReadWriteModel<T> : IParentRelationshipSetter, IParentRelationship<T>
    {
        public T Parent { get; set; }

        public object ParentSetter { set => Parent = (T)value; }


    }
}
