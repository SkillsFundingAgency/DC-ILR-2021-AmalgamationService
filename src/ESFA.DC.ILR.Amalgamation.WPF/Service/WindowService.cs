using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Views;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class WindowService : IWindowService
    {
        public void ShowSettingsWindow()
        {
            var settingsWindow = new SettingsWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            settingsWindow.ShowDialog();
        }

        public void ShowAboutWindow()
        {
            var aboutWindow = new AboutWindow()
            {
                Owner = Application.Current.MainWindow,
            };

            aboutWindow.ShowDialog();
        }
    }
}
