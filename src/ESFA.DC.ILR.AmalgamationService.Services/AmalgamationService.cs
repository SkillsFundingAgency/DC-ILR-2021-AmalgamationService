using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.Model.Interface;
using ESFA.DC.ILR.Model.Loose;
using ESFA.DC.Serialization.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace ESFA.DC.ILR.AmalgamationService.Services
{
    public class AmalgamationService : IAmalgamationService
    {
        public Task<IAmalgamationMessage> AmalgamateAsync(List<Message> messages)
        {
            throw new System.NotImplementedException();
        }
    }
}
