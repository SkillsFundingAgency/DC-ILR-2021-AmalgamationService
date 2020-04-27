namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseProviderSpecDeliveryMonitoring : IParentRelationship<ILooseLearningDelivery>, IAmalgamationModel
    {
        string ProvSpecDelMonOccur { get;  set; }

        string ProvSpecDelMon { get;  set; }
    }
}
