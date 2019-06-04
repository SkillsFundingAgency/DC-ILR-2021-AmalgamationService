using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.AmalgamationService.Interface;
using ESFA.DC.ILR.AmalgamationService.Modules;
using ESFA.DC.ILR.AmalgamationService.Stubs;
using Autofac;
namespace ESFA.DC.ILR.AmalgamationService.Console
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var argsList = args.ToList();

            if (!argsList.Any())
            {
                argsList.Add(@"TestFiles/ILR-10000001-1819-20180819-112505-01.xml");
                argsList.Add(@"TestFiles/ILR-10000001-1819-20180819-112505-02.xml");
                argsList.Add(@"TestFiles/ILR-10000001-1819-20180819-112505-03.xml");
            }

            RunAmalgamation(argsList).GetAwaiter().GetResult();
        }

        private static async Task RunAmalgamation(List<string> files)
        {
            var amalgamationContext = new AmalgamationContextStub { FileNames = files };

            var container = BuildContainer();

            using (var scope = container.BeginLifetimeScope(c => RegisterContext(c, amalgamationContext)))
            {
                var preValidationOrchestrationService = scope.Resolve<IAmalgamationOrchestrationService<IAmalgamationContext>>();

                await preValidationOrchestrationService.ExecuteAsync(amalgamationContext, CancellationToken.None);
            }
        }

        private static void RegisterContext(ContainerBuilder containerBuilder, IAmalgamationContext amalgamationContext)
        {
            containerBuilder.RegisterInstance(amalgamationContext).As<IAmalgamationContext>();
        }

        private static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<ConsoleAmalgamationServiceModule>();

            return containerBuilder.Build();
        }
    }
}
