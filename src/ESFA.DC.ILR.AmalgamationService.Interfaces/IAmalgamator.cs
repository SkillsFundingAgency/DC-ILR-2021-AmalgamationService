﻿using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamator<T>
    {
        T Amalgamate(IEnumerable<T> models);
    }
}
