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
    public class MainViewModel : ViewModelBase
    {
        private readonly IAmalgamationOrchestrationService _iAmalgamationOrchestrationService;

        private readonly IMessengerService _messengerService;
        private readonly IDialogInteractionService _dialogInteractionService;

        private CancellationTokenSource _cancellationTokenSource;
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
            AmalgamateFilesCommand = new AsyncCommand(AmalgamateFiles, () => CanSubmit);

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
            }
        }

        public bool CanSubmit
        {
            get => _canSubmit;
            set
            {
                Set(ref _canSubmit, value);
                AmalgamateFilesCommand.RaiseCanExecuteChanged();
            }
        }

        public bool ChooseFileVisibility => CurrentStage == StageKeys.ChooseFile;

        public bool ProcessingVisibility => CurrentStage == StageKeys.Processing;

        public bool ProcessedSuccessfullyVisibility => CurrentStage == StageKeys.ProcessedSuccessfully;

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

        public AsyncCommand AmalgamateFilesCommand { get; set; }

        public RelayCommand ReportsFolderCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public void HandleTaskProgressMessage(TaskProgressMessage taskProgressMessage)
        {
            TaskName = taskProgressMessage.TaskName;
            CurrentTask = taskProgressMessage.CurrentTask;
            TaskCount = taskProgressMessage.TaskCount;
        }

        private void ShowChooseFileDialog()
        {
            var fileName = _dialogInteractionService.GetFileNameFromOpenFileDialog();

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                files.Add(fileName);
                if (files.Count > 1)
                {
                    CanSubmit = true;
                }
            }
        }

        private async Task AmalgamateFiles()
        {
            CurrentStage = StageKeys.Processing;
            CanSubmit = false;
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                CancelCommand.RaiseCanExecuteChanged();

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

        private void Cancel()
        {
            TaskName = "Cancelling";
            _cancellationTokenSource?.Cancel();

            CancelCommand.RaiseCanExecuteChanged();
        }
    }
}