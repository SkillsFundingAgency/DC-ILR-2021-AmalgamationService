namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseEmploymentStatusMonitoring : IParentRelationship<ILooseLearner>, IAmalgamationModel
    {
        string ESMType { get;  set; }

        long? ESMCodeNullable { get;  set; }
    }
}
