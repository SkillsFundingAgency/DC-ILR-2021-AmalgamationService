namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearningProvider : IParentRelationship<ILooseMessage>, IAmalgamationModel
    {
        int UKPRN { get;  set; }
    }
}
