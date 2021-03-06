﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System.Collections.Generic;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationResult : IAmalgamationResult
    {
        public Message Message { get; set; }

        public IEnumerable<IAmalgamationValidationError> ValidationErrors { get; set; }
    }
}
