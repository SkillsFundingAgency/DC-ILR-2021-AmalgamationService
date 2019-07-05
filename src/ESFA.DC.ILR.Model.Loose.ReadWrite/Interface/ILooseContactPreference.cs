namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseContactPreference : IParentRelationship<ILooseLearner>, IAmalgamationModel
    {
        string ContPrefType { get;  set; }

        long? ContPrefCodeNullable { get;  set; }
    }
}
