namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseEmploymentStatusMonitoring : IParentRelationship<ILooseLearnerEmploymentStatus>, IAmalgamationModel
    {
        string ESMType { get;  set; }

        long? ESMCodeNullable { get;  set; }
    }
}
