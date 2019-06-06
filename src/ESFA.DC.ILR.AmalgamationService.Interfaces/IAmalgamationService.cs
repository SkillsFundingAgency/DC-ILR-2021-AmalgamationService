using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Model.Loose;
namespace ESFA.DC.ILR.AmalgamationService.Interfaces
{
    public interface IAmalgamationService
    {
        Task<IAmalgamationMessage> AmalgamateAsync(List<Message> messages,CancellationToken cancellationToken);
    }
}
