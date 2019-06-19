﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IRuleProvider
    {
        IRule<T> BuildStandardRule<T>();

        IRule<string> BuildAddressRule();
    }
}
