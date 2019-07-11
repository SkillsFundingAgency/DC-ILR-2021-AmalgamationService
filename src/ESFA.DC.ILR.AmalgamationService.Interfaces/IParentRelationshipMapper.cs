using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IParentRelationshipMapper
    {
        T MapChildren<T>(T input);
    }
}
