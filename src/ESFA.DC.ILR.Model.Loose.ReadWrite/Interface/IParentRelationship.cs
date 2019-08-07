namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface IParentRelationship<T> : IParentRelationshipSetter
    {
        T Parent { get; set; }
    }
}
