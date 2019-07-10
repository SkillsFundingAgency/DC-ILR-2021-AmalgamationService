using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Model.Loose.ReadWrite;

namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationService
    {
        Task<IAmalgamationResult> AmalgamateAsync(IEnumerable<Message> messages, CancellationToken cancellationToken);
    }
}
