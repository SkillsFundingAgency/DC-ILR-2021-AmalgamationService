using System.Collections.Generic;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationResult
    {
        Message Messaage { get; }

        IEnumerable<IAmalgamationValidationError> ValidationErrors { get; }
    }
}
