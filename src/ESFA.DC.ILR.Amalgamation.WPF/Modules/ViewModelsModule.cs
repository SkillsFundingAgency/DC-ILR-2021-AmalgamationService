using Autofac;
using ESFA.DC.ILR.Amalgamation.WPF.ViewModel;

namespace ESFA.DC.ILR.Amalgamation.WPF.Modules
{
    public class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<MainViewModel>().SingleInstance();
        }
    }
}
