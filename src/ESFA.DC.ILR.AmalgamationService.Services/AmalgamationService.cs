using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Loose;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationService : IAmalgamationService
    {
        public Task<IAmalgamationResult> AmalgamateAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
