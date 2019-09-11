using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.Amalgamation.WPF.Modules;
using ESFA.DC.ILR.Amalgamation.WPF.Service;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Json;
using ESFA.DC.Serialization.Xml;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class PerformanceTest
    {
        private const string _testDataDirectory = @"..\..\Test Files";
        IAmalgamationManagementService _amalgamationManagementService;
        IFileService _fileService;
        IJsonSerializationService _jsonSerializationService;
        IDateTimeProvider _dateTimeProvider;

        [Fact]
        public async void TimeTracker()
        {
            var containerBuilder = BuildContainerBuilder();

            var container = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
            _amalgamationManagementService = container.Resolve<IAmalgamationManagementService>();
            _fileService = container.Resolve<IFileService>();
            _jsonSerializationService = container.Resolve<IJsonSerializationService>();
            _dateTimeProvider = container.Resolve<IDateTimeProvider>();

            var cancellationToken = new CancellationToken();

            Stopwatch timer = new Stopwatch();
            timer.Start();
            await _amalgamationManagementService.ProcessAsync(Directory.GetFiles(_testDataDirectory, "*.xml"), _testDataDirectory, cancellationToken);
            timer.Stop();

            var outputPath = new DirectoryInfo(_testDataDirectory).GetDirectories().OrderByDescending(d => d.LastWriteTimeUtc).FirstOrDefault().FullName;

            using (var stream = await _fileService.OpenWriteStreamAsync($"TimeTracker{_dateTimeProvider.GetNowUtc().ToString("yyyyMMdd-HHmmss")}.txt", outputPath, cancellationToken))
            {
                var timeConsumed = $" total time elapsed to file merge {timer.Elapsed.TotalSeconds} seconds";
                _jsonSerializationService.Serialize(timeConsumed, stream);
            }
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
            containerBuilder.RegisterType<JsonSerializationService>().As<IJsonSerializationService>();

            containerBuilder.RegisterType<FileSystemFileService>().As<IFileService>();

            return containerBuilder;
        }
    }
}
