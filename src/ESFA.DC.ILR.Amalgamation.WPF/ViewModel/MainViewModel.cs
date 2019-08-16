using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.Amalgamation.WPF.Command;
using ESFA.DC.ILR.Amalgamation.WPF.Enum;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Message;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;
using ESFA.DC.Logging.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ESFA.DC.ILR.Amalgamation.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string OutputDirectoryKey = "OutputDirectory";

        private readonly IAmalgamationManagementService _amalgamationManagementService;

        private readonly IMessengerService _messengerService;
        private readonly IWindowService _windowService;
        private readonly IDialogInteractionService _dialogInteractionService;
        private readonly IWindowsProcessService _windowsProcessService;
        private readonly ILogger _logger;

        private CancellationTokenSource _cancellationTokenSource;
        private ObservableCollection<string> _files = new ObservableCollection<string>();
        private bool _canMerge;
        private string _taskName;
        private int _currentTask;
        private int _taskCount = 1;
        private StageKeys _currentStage = StageKeys.ChooseFile;
        private bool _showErrorMessage;

        public MainViewModel(
            IAmalgamationManagementService iAmalgamationManagementService,
            IMessengerService messengerService,
            IWindowService windowService,
            IDialogInteractionService dialogInteractionService,
            IWindowsProcessService windowsProcessService,
            ILogger logger)
        {
            _amalgamationManagementService = iAmalgamationManagementService;
            _messengerService = messengerService;
            _windowService = windowService;
            _dialogInteractionService = dialogInteractionService;
            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
            _windowsProcessService = windowsProcessService;
            _logger = logger;

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog);
            AmalgamateFilesCommand = new AsyncCommand(AmalgamateFiles, () => CanMerge);
            OutputDirectoryCommand = new RelayCommand(() => ProcessStart(OutputDirectory));
            SettingsNavigationCommand = new RelayCommand(SettingsNavigate);
            AboutNavigationCommand = new RelayCommand(AboutNavigate);
            RemoveFileCommand = new RelayCommand<object>(RemoveFile);

            CancelCommand = new RelayCommand(Cancel, () => (CurrentStage == StageKeys.Processing) && (!_cancellationTokenSource?.IsCancellationRequested ?? false));
        }

        public StageKeys CurrentStage
        {
            get => _currentStage;
            set
            {
                _currentStage = value;

                RaisePropertyChanged(nameof(ChooseFileVisibility));
                RaisePropertyChanged(nameof(ProcessingVisibility));
                RaisePropertyChanged(nameof(ProcessedSuccessfullyVisibility));
            }
        }

        public bool CanMerge
        {
            get => _canMerge;
            set
            {
                Set(ref _canMerge, value);
                if (_canMerge)
                {
                    ShowErrorMessage = false;
                }

                AmalgamateFilesCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ChooseFileVisibility => CurrentStage == StageKeys.ChooseFile;

        public bool ProcessingVisibility => CurrentStage == StageKeys.Processing;

        public bool ProcessedSuccessfullyVisibility => CurrentStage == StageKeys.ProcessedSuccessfully;

        public ObservableCollection<string> Files
        {
            get { return _files; }

            set
            {
                if (value != _files)
                {
                    _files = value;
                }

                RaisePropertyChanged(nameof(Files));
            }
        }

        public string OutputDirectory
        {
            get => ConfigurationManager.AppSettings[OutputDirectoryKey];
        }

        public string TaskName
        {
            get => _taskName;
            set => Set(ref _taskName, value);
        }

        public int CurrentTask
        {
            get => _currentTask;
            set => Set(ref _currentTask, value);
        }

        public int TaskCount
        {
            get => _taskCount;
            set => Set(ref _taskCount, value);
        }

        public bool ShowErrorMessage
        {
            get { return _showErrorMessage; }

            set
            {
                Set(ref _showErrorMessage, value);
            }
        }

        public RelayCommand ChooseFileCommand { get; set; }

        public RelayCommand<object> RemoveFileCommand { get; set; }

        public AsyncCommand AmalgamateFilesCommand { get; set; }

        public RelayCommand OutputDirectoryCommand { get; set; }

        public RelayCommand SettingsNavigationCommand { get; set; }

        public RelayCommand AboutNavigationCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        private void ShowChooseFileDialog()
        {
            CurrentStage = StageKeys.ChooseFile;
            var selectedFiles = _dialogInteractionService.GetFileNamesFromOpenFileDialog();

            if (selectedFiles?.Length > 0)
            {
                selectedFiles.ToList().ForEach(x => Files.Add(x));
                CanMerge = Files.Count > 1;
            }
        }

        private async Task AmalgamateFiles()
        {
            CurrentStage = StageKeys.Processing;
            CanMerge = false;
            ShowErrorMessage = false;

            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                CancelCommand.RaiseCanExecuteChanged();

                var result = await _amalgamationManagementService.ProcessAsync(_files, OutputDirectory, _cancellationTokenSource.Token);

                if (!result)
                {
                    ShowErrorMessage = true;
                    CurrentStage = StageKeys.ChooseFile;
                    return;
                }

                CurrentStage = StageKeys.ProcessedSuccessfully;
            }
            catch (OperationCanceledException operationCanceledException)
            {
                _logger.LogError("Operation Cancelled", operationCanceledException);

                CurrentStage = StageKeys.ChooseFile;
            }
            finally
            {
                Files.Clear();
                CancelCommand.RaiseCanExecuteChanged();
                _cancellationTokenSource.Dispose();
            }
        }

        private void Cancel()
        {
            TaskName = "Cancelling";

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource?.Cancel();
            }

            CancelCommand.RaiseCanExecuteChanged();
        }

        private void ProcessStart(string url)
        {
            _windowsProcessService.ProcessStart(url);
        }

        private void SettingsNavigate()
        {
            _windowService.ShowSettingsWindow();
        }

        private void AboutNavigate()
        {
            _windowService.ShowAboutWindow();
        }

        private void RemoveFile(object file)
        {
            Files.Remove(file.ToString());
            CanMerge = Files.Count > 1;
        }
    }
}