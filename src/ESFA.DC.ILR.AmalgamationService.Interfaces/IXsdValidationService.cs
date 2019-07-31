using System.IO;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IXsdValidationService
    {
        bool ValidateSchema(string xmlFileName);
    }
}
