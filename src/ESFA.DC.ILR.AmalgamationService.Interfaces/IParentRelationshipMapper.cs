namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IParentRelationshipMapper
    {
        T MapChildren<T>(T input);
    }
}
