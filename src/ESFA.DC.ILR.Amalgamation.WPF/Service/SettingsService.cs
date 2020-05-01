using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class SettingsService : ISettingsService
    {
        private const string OutputDirectoryKey = "OutputDirectory";
        private const string AmalgamationSystem = "FileMerge 2020-21";

        public string OutputDirectory { get; set; }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            OutputDirectory = ConfigurationManager.AppSettings[OutputDirectoryKey];

            if (string.IsNullOrWhiteSpace(OutputDirectory))
            {
                OutputDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), AmalgamationSystem);
                await SaveAsync(cancellationToken);
            }
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Clear();
            config.AppSettings.Settings.Add(OutputDirectoryKey, OutputDirectory);

            config.Save(ConfigurationSaveMode.Modified);

            return Task.CompletedTask;
        }
    }
}
