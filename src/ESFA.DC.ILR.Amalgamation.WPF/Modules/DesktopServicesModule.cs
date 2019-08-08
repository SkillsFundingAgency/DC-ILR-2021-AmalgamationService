using System.Threading;
using Autofac;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Service;
using ESFA.DC.ILR.Amalgamation.WPF.Service.Interface;

namespace ESFA.DC.ILR.Amalgamation.WPF.Modules
{
    public class DesktopServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MessengerService>().As<IMessengerService>().SingleInstance();
            containerBuilder.RegisterType<WindowService>().As<IWindowService>();
            containerBuilder.RegisterType<DialogInteractionService>().As<IDialogInteractionService>();
            containerBuilder.RegisterType<WindowsProcessService>().As<IWindowsProcessService>();
            containerBuilder.RegisterType<VersionInformationService>().As<IVersionInformationService>();

            containerBuilder.Register(c =>
            {
                var settings = new SettingsService();
                settings.LoadAsync(CancellationToken.None).Wait();
                return settings;
            }).As<ISettingsService>().SingleInstance();
        }
    }
}
