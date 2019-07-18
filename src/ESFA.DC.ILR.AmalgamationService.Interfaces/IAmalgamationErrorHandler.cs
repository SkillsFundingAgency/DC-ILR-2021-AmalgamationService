using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationErrorHandler
    {
        IReadOnlyCollection<IAmalgamationValidationError> Errors { get; }

        void HandleErrors(IEnumerable<IAmalgamationValidationError> errors);
    }
}
