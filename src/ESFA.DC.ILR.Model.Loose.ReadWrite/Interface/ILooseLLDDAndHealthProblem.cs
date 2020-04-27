namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLLDDAndHealthProblem : IParentRelationship<ILooseLearner>, IAmalgamationModel
    {
        long? LLDDCatNullable { get;  set; }

        long? PrimaryLLDDNullable { get;  set; }
    }
}
