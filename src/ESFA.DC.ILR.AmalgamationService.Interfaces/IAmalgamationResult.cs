using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Model.Loose;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationResult
    {
        Message Messaage { get; }
        IEnumerable<IAmalgamationValidationError> ValidationErrors { get; }
    }
}
