using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRule<T>
    {
        IRuleResult<T> Definition(IEnumerable<T> values);
    }
}
