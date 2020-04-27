using System.Windows;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Views
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window, ICloseable
    {
        public AboutWindow()
        {
            InitializeComponent();
        }
    }
}
