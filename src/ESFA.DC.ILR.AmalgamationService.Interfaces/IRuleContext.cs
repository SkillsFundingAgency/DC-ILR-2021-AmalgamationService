namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleContext
    {
        string FileName { get; set; }

        string LearnRefNumber { get; set; }

        string Entity { get; set; }

        string Key { get; set; }
    }
}
