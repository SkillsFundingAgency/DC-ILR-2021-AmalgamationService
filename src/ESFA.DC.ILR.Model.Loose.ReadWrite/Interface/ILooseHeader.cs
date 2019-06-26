namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseHeader
    {
        ILooseCollectionDetails CollectionDetailsEntity { get;  set; }

        ILooseSource SourceEntity { get;  set; }
    }
}
