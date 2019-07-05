namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseProviderSpecLearnerMonitoring : IParentRelationship<ILooseLearner>, IAmalgamationModel
    {
        string ProvSpecLearnMonOccur { get;  set; }
        
        string ProvSpecLearnMon { get;  set; }
    }
}
