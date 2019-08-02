using System;
using System.Collections.Generic;
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
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ESFA.DC.ILR.Amalgamation.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const string OutputDirectoryKey = "OutputDirectory";

        private readonly IAmalgamationManagementService _iAmalgamationManagementService;

        private readonly IMessengerService _messengerService;
        private readonly IDialogInteractionService _dialogInteractionService;
        private readonly IWindowsProcessService _windowsProcessService;

        private CancellationTokenSource _cancellationTokenSource;
        private ObservableCollection<string> _files = new ObservableCollection<string>();
        private bool _canSubmit;
        private string _taskName;
        private int _currentTask;
        private int _taskCount = 1;
        private StageKeys _currentStage = StageKeys.ChooseFile;
        private string _outputDirectory;
        private bool _showErrorMessage;

        public MainViewModel(
            IAmalgamationManagementService iAmalgamationManagementService,
            IMessengerService messengerService,
            IDialogInteractionService dialogInteractionService,
            IWindowsProcessService windowsProcessService)
        {
            _iAmalgamationManagementService = iAmalgamationManagementService;
            _messengerService = messengerService;
            _dialogInteractionService = dialogInteractionService;
            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);
            _windowsProcessService = windowsProcessService;

            _outputDirectory = ConfigurationManager.AppSettings[OutputDirectoryKey];

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog);
            AmalgamateFilesCommand = new AsyncCommand(AmalgamateFiles, () => CanSubmit);
            OutputDirectoryCommand = new RelayCommand(() => ProcessStart(_outputDirectory));

            CancelCommand = new RelayCommand(Cancel, () => !_cancellationTokenSource?.IsCancellationRequested ?? false);
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

        public bool CanSubmit
        {
            get => _canSubmit;
            set
            {
                Set(ref _canSubmit, value);
                if (_canSubmit)
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
            get => _outputDirectory;
            set => Set(ref _outputDirectory, value);
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

        public RelayCommand RemoveFileCommand { get; set; }

        public AsyncCommand AmalgamateFilesCommand { get; set; }

        public RelayCommand OutputDirectoryCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        private void ShowChooseFileDialog()
        {
            Files = new ObservableCollection<string>();
            var selectedFiles = _dialogInteractionService.GetFileNamesFromOpenFileDialog();

            if (selectedFiles?.Length > 0)
            {
                selectedFiles.ToList().ForEach(x => Files.Add(x));
                CanSubmit = Files.Count > 1;
            }
        }

        private async Task AmalgamateFiles()
        {
            CurrentStage = StageKeys.ChooseFile;
            if (!_iAmalgamationManagementService.IsValidSchema(_files))
            {
                ShowErrorMessage = true;
                return;
            }

            CurrentStage = StageKeys.Processing;
            CanSubmit = false;
            ShowErrorMessage = false;
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                CancelCommand.RaiseCanExecuteChanged();

                await _iAmalgamationManagementService.ProcessAsync(_files, _outputDirectory, _cancellationTokenSource.Token);
                CurrentStage = StageKeys.ProcessedSuccessfully;
            }
            catch (Exception ex)
            {
                // Error handling
                var str = ex.InnerException;
                CurrentStage = StageKeys.ChooseFile;
            }
            finally
            {
                Files.Clear();
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
    }
}