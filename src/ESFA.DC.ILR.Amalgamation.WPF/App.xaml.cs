﻿using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Modules;
using ESFA.DC.ILR.Amalgamation.WPF.Service;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.Amalgamation.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var containerBuilder = BuildContainerBuilder();

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));

            base.OnStartup(e);
        }

        private ContainerBuilder BuildContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<LoggingModule>();
            containerBuilder.RegisterModule<DesktopServicesModule>();
            containerBuilder.RegisterModule<AmalgamationServiceModule>();
            containerBuilder.RegisterModule<ViewModelsModule>();

            // Common Service Registration
            containerBuilder.RegisterType<AmalgamationManagementService>().As<IAmalgamationManagementService>();
            containerBuilder.RegisterType<XmlSerializationService>().As<IXmlSerializationService>();

            containerBuilder.RegisterType<FileSystemFileService>().As<IFileService>();

            return containerBuilder;
        }
    }
}
