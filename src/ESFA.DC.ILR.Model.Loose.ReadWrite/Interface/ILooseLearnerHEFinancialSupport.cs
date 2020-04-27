namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearnerHEFinancialSupport : IParentRelationship<ILooseLearnerHE>, IAmalgamationModel
    {
        long? FINTYPENullable { get;  set; }

        long? FINAMOUNTNullable { get;  set; }
    }
}
