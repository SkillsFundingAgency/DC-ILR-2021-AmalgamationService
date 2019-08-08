using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Amalgamation.WPF.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        private IVersionInformationService _versionInformationService;

        public AboutViewModel(IVersionInformationService versionInformationService)
        {
            _versionInformationService = versionInformationService;

            AboutItems = new ObservableCollection<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Version Number:", _versionInformationService.VersionNumber),
            };
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
        }

        public RelayCommand<ICloseable> CloseWindowCommand { get; }

        public ObservableCollection<KeyValuePair<string, string>> AboutItems { get; }

        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}
