using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamator<T>
    {
        T Amalgamate(IEnumerable<T> models);
    }
}
