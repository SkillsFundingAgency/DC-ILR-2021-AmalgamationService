using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRule<T>
    {
        T Definition(IEnumerable<T> values);
    }
}
