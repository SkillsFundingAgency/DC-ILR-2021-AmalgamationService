using System.Windows;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, ICloseable
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }
    }
}
