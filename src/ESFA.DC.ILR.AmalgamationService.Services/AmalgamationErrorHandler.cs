using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationErrorHandler : IAmalgamationErrorHandler
    {
        private ConcurrentBag<IAmalgamationValidationError> _errors = new ConcurrentBag<IAmalgamationValidationError>();

        public IReadOnlyCollection<IAmalgamationValidationError> Errors => _errors;

        public void HandleErrors(IEnumerable<IAmalgamationValidationError> errors)
        {
            foreach (var error in errors)
            {
                _errors.Add(error);
            }
        }
    }
}
