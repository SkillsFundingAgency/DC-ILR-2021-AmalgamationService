using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Autofac;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.FileService;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Modules;
using ESFA.DC.ILR.Amalgamation.WPF.Service;
using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services;
using ESFA.DC.ILR.AmalgamationService.Services.CrossValidation;
using ESFA.DC.ILR.AmalgamationService.Services.Rules.Factory;
using ESFA.DC.ILR.AmalgamationService.Services.Validation;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Schema;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Schema.Interface;
using ESFA.DC.IO.FileSystem;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;
using ESFA.DC.Serialization.Xml;
using FluentAssertions;
using Xunit;
using amalgamators = ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ilrModelRw = ESFA.DC.ILR.Model.Loose.ReadWrite;
using service = ESFA.DC.ILR.AmalgamationService.Services;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class PerformanceTest
    {
        private const string TestDataDirectory = @"..\..\Test Files";
        IAmalgamationManagementService _amalgamationManagementService;

        //[Fact]
        public async void TimeTracker()
        {
            var containerBuilder = BuildContainerBuilder();

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
            container.TryResolve<IAmalgamationManagementService>(out _amalgamationManagementService);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            await _amalgamationManagementService.ProcessAsync(Directory.GetFiles(TestDataDirectory), TestDataDirectory, new CancellationToken());
            timer.Stop();

            Console.WriteLine($" total time elapsed to file merge {timer.Elapsed.TotalSeconds}");
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
