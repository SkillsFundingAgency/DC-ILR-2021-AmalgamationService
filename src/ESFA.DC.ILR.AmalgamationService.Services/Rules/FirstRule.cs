using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class FirstRule<T> : IRule<T>
    {
        public T Definition(IEnumerable<T> values)
        {
            return values.FirstOrDefault();
        }
    }
}
