namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearnerFAM : IParentRelationship<ILooseLearner>, IAmalgamationModel
    {
        string LearnFAMType { get;  set; }
        long? LearnFAMCodeNullable { get;  set; }
    }
}
