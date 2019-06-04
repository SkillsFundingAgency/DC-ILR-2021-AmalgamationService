using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ESFA.DC.ILR.Model.Interface;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IMessageProvider<T>
    {
        Task<T> ProvideAsync(string file, CancellationToken cancellationToken);
    }
}
