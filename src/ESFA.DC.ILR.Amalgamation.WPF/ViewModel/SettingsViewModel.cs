using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Amalgamation.WPF.Command;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ESFA.DC.ILR.Amalgamation.WPF.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private const string OutputDirectoryDescription = "Choose Output Directory";
        private readonly ISettingsService _settingsService;
        private readonly IDialogInteractionService _dialogInteractionService;
        private string _outputDirectory;

        public SettingsViewModel(ISettingsService settingsService, IDialogInteractionService dialogInteractionService)
        {
            _dialogInteractionService = dialogInteractionService;
            _settingsService = settingsService;

            _outputDirectory = settingsService.OutputDirectory;

            ChooseOutputDirectoryCommand = new RelayCommand(ChooseOutputDirectory);
            SaveSettingsCommand = new AsyncCommand<ICloseable>(SaveSettings, CanSave);
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
        }

        public RelayCommand ChooseOutputDirectoryCommand { get; set; }

        public AsyncCommand<ICloseable> SaveSettingsCommand { get; set; }

        public RelayCommand<ICloseable> CloseWindowCommand { get; set; }

        public string OutputDirectory
        {
            get => _outputDirectory;
            set
            {
                _outputDirectory = value;
                RaisePropertyChanged();
                SaveSettingsCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(OutputDirectory);
        }

        private void ChooseOutputDirectory()
        {
            var directory = _dialogInteractionService.GetFolderNameFromFolderBrowserDialog(_settingsService.OutputDirectory, OutputDirectoryDescription);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                OutputDirectory = directory;
            }
        }

        private async Task SaveSettings(ICloseable window)
        {
            _settingsService.OutputDirectory = OutputDirectory;

            await _settingsService.SaveAsync(CancellationToken.None);

            window?.Close();
        }

        private void CloseWindow(ICloseable window)
        {
            OutputDirectory = _settingsService.OutputDirectory;
            window?.Close();
        }
    }
}
