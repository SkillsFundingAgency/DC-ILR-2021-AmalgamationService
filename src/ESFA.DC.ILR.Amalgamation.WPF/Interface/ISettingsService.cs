using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Amalgamation.WPF.Interface
{
    public interface ISettingsService
    {
        string OutputDirectory { get; set; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
