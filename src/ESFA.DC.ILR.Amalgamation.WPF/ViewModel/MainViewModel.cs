using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private const string _filenamePlaceholder = "No file chosen";

        private readonly IAmalgamationOrchestrationService _iAmalgamationOrchestrationService;

        private readonly IMessengerService _messengerService;
        private readonly IDialogInteractionService _dialogInteractionService;

        private CancellationTokenSource _cancellationTokenSource;
        private string _fileName = _filenamePlaceholder;
        private ObservableCollection<string> files = new ObservableCollection<string>();
        private bool _canSubmit;
        private string _taskName;
        private int _currentTask;
        private int _taskCount = 1;
        private StageKeys _currentStage = StageKeys.ChooseFile;
        private string _reportsLocation;

        public MainViewModel(
            IAmalgamationOrchestrationService iAmalgamationOrchestrationService,
            IMessengerService messengerService,
            IDialogInteractionService dialogInteractionService)
        {
            _iAmalgamationOrchestrationService = iAmalgamationOrchestrationService;
            _messengerService = messengerService;
            _dialogInteractionService = dialogInteractionService;
            _messengerService.Register<TaskProgressMessage>(this, HandleTaskProgressMessage);

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => CanSubmit);

            CancelAndReuploadCommand = new RelayCommand(CancelAndReupload, () => !_cancellationTokenSource?.IsCancellationRequested ?? false);
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
                RaisePropertyChanged(nameof(ProcessFailureHandledVisibility));
                RaisePropertyChanged(nameof(ProcessFailureUnhandledVisibility));
            }
        }

        public bool CanSubmit
        {
            get => _canSubmit;
            set
            {
                Set(ref _canSubmit, value);
                ProcessFileCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ChooseFileVisibility => CurrentStage == StageKeys.ChooseFile;

        public bool ProcessingVisibility => CurrentStage == StageKeys.Processing;

        public bool ProcessedSuccessfullyVisibility => CurrentStage == StageKeys.ProcessedSuccessfully;

        public bool ProcessFailureHandledVisibility => CurrentStage == StageKeys.ProcessHandledFailure;

        public bool ProcessFailureUnhandledVisibility => CurrentStage == StageKeys.ProcessUnhandledFailure;

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> Files
        {
            get { return files; }

            set
            {
                if (value != this.files)
                    files = value;
                this.RaisePropertyChanged("Files");
            }
        }

        public string ReportsLocation
        {
            get => _reportsLocation;
            set => Set(ref _reportsLocation, value);
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

        public RelayCommand ChooseFileCommand { get; set; }

        public RelayCommand RemoveFileCommand { get; set; }

        public AsyncCommand ProcessFileCommand { get; set; }

        public RelayCommand ReportsFolderCommand { get; set; }

        public RelayCommand CancelAndReuploadCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        private void ShowChooseFileDialog()
        {
            var fileName = _dialogInteractionService.GetFileNameFromOpenFileDialog();

            if (!string.IsNullOrWhiteSpace(fileName) && fileName != _filenamePlaceholder)
            {
                files.Add(fileName);
                if (files.Count > 1)
                {
                    CanSubmit = true;
                }
            }
        }

        private async Task ProcessFile()
        {
            CurrentStage = StageKeys.Processing;
            CanSubmit = false;
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                CancelAndReuploadCommand.RaiseCanExecuteChanged();

                await _iAmalgamationOrchestrationService.ProcessAsync(new List<string>(files), _reportsLocation, _cancellationTokenSource.Token);
            }
            catch (Exception ex) //OperationCanceledException operationCanceledException)
            {
                // Error handling
                var str = ex.InnerException;
                CurrentStage = StageKeys.ChooseFile;
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }
        }

        private void CancelAndReupload()
        {
            TaskName = "Cancelling";
            _cancellationTokenSource?.Cancel();

            CancelAndReuploadCommand.RaiseCanExecuteChanged();
        }
    }
}