using System.Collections.Generic;
using ESFA.DC.ILR.AmalgamationService.Interface;

namespace ESFA.DC.ILR.AmalgamationService.Stubs
{
    public class AmalgamationContextStub : IAmalgamationContext
    {
        public List<string> FileNames { get; set; }

        public string Container { get; set; }

        public string JobId { get; set; }

        public IEnumerable<string> IValidationErrors { get; set; }
    }
}
