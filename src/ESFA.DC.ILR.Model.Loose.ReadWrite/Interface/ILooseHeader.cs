namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseHeader : IParentRelationship<ILooseMessage>
    {
        ILooseCollectionDetails CollectionDetailsEntity { get;  set; }

        ILooseSource SourceEntity { get;  set; }

        ILooseMessage Message { get; set; }
    }
}
