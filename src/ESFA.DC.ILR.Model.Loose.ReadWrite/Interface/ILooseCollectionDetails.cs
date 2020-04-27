using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseCollectionDetails : IParentRelationship<ILooseHeader>, IAmalgamationModel
    {
        string CollectionString { get;  set; }

        string YearString { get;  set; }

        DateTime FilePreparationDate { get;  set; }
    }
}
