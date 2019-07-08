namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseHeader : IParentRelationship<ILooseMessage>, IAmalgamationModel
    {
        ILooseCollectionDetails CollectionDetailsEntity { get;  set; }

        ILooseSource SourceEntity { get;  set; }
    }
}
