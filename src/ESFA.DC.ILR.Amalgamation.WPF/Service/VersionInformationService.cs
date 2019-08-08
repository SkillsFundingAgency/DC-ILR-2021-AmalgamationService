using System.Reflection;
using ESFA.DC.ILR.Amalgamation.WPF.Config;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class VersionInformationService : IVersionInformationService
    {
        public string Date => DesktopServiceConfiguration.Configuration.ReleaseDate;

        public string VersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
