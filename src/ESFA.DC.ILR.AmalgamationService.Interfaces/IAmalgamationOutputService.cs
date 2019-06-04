using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ESFA.DC.ILR.Model.Interface;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationOutputService<T>
    {
        Task<T> ProcessAsync(List<IMessage> messages, string outputFilePath, CancellationToken cancellationToken);
    }
}
