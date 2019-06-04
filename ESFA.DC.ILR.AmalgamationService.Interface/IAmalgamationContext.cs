using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interface
{
    public interface IAmalgamationContext
    {
        List<string> FileNames { get; }

        string Container { get; }

        string JobId { get; }

        IEnumerable<string> IValidationErrors { get; }
    }
}
