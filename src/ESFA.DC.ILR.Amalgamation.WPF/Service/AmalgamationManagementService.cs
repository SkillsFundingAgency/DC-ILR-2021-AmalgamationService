using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ILR.Amalgamation.WPF.Interface;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.Amalgamation.WPF.Service
{
    public class AmalgamationManagementService : IAmalgamationManagementService
    {
        private const string _assemblyName = "ESFA.DC.ILR.Model.Loose.ReadWrite";
        private const string _xsdResourceName = ".Schema.ILR-2019-20-v2.xsd";

        private readonly ILifetimeScope _lifetimeScope;

        public AmalgamationManagementService(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public async Task ProcessAsync(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(
                () => ExecuteAsyncAction(filePaths, outputPath, cancellationToken),
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

            return;
        }

        public bool IsValidSchema(IEnumerable<string> filePaths)
        {
            bool validSchema = false;

            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope())
            {
                var xsdValidationService = executionLifetimeScope.Resolve<IXsdValidationService>();
                foreach (var file in filePaths)
                {
                    validSchema = xsdValidationService.ValidateSchema(file);

                    if (!validSchema)
                    {
                        break;
                    }
                }
            }

            return validSchema;
        }

        private async Task ExecuteAsyncAction(IEnumerable<string> filePaths, string outputPath, CancellationToken cancellationToken)
        {
            using (var executionLifetimeScope = _lifetimeScope.BeginLifetimeScope())
            {
                await executionLifetimeScope.Resolve<IAmalgamationOrchestrationService>().ProcessAsync(filePaths, outputPath, cancellationToken);
                return;
            }
        }
    }
}
