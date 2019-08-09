using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface ICrossValidationService
    {
        ILooseMessage CrossValidateLearners(ILooseMessage message);
    }
}
