using System.Threading;
using Autofac;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Message;
using ESFA.DC.ILR.Amalgamation.WPF.Service;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Modules
{
    public class DesktopServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MessengerService>().As<IMessengerService>().SingleInstance();
            containerBuilder.RegisterType<DialogInteractionService>().As<IDialogInteractionService>();
            containerBuilder.RegisterType<WindowsProcessService>().As<IWindowsProcessService>();
        }
    }
}
