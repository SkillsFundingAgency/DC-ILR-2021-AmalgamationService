using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IInvalidRecordRemovalService
    {
        IAmalgamationResult RemoveInvalidLearners(IAmalgamationResult amalgamationResult);
    }
}
