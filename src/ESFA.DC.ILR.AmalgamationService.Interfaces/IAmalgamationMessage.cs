using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Model.Loose;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationMessage
    {
        Message messaage { get; }
        IAmalgamationValidationError validationError { get; }
    }
}
