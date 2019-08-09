using Autofac;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Interface;
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
using ESFA.DC.Serialization.Xml;
using amalgamators = ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ilrModelRw = ESFA.DC.ILR.Model.Loose.ReadWrite;
using service = ESFA.DC.ILR.AmalgamationService.Services;

namespace ESFA.DC.ILR.Amalgamation.WPF.Modules
{
    public class AmalgamationServiceModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AmalgamationErrorHandler>().As<IAmalgamationErrorHandler>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<ParentRelationshipMapper>().As<IParentRelationshipMapper>();
            containerBuilder.RegisterType<CsvService>().As<ICsvService>();

            // XSD Validation
            containerBuilder.RegisterType<SchemaProvider>().As<ISchemaProvider>();
            containerBuilder.RegisterType<ValidationErrorHandler>().As<IValidationErrorHandler>();
            containerBuilder.RegisterType<XsdValidationService>().As<IXsdValidationService>();

            // Cross Record Validation
            containerBuilder.RegisterType<CrossValidationService>().As<ICrossValidationService>();

            containerBuilder.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();

            containerBuilder.RegisterType<AmalgamationOrchestrationService>().As<IAmalgamationOrchestrationService>();

            // AmalgamationOrchestrationService dependencies
            containerBuilder.RegisterType<MessageProvider>().As<IMessageProvider<ilrModelRw.AmalgamationRoot>>();
            containerBuilder.RegisterType<service.AmalgamationService>().As<IAmalgamationService>();
            containerBuilder.RegisterType<AmalgamationOutputService>().As<IAmalgamationOutputService>();

            // MessageProvider dependencies
            containerBuilder.RegisterType<XmlSerializationService>().As<IXmlSerializationService>();
            containerBuilder.RegisterType<FileSystemFileService>().As<IFileService>();
            containerBuilder.RegisterType<FileSystemKeyValuePersistenceService>()
                .As<IKeyValuePersistenceService>()
                .As<IStreamableKeyValuePersistenceService>();

            containerBuilder.RegisterType<DecompressionService>().As<IDecompressionService>();

            containerBuilder.RegisterType<RuleProvider>().As<IRuleProvider>();

            containerBuilder.RegisterType<amalgamators.MessageAmalgamator>().As<IAmalgamator<ilrModelRw.Message>>();
            containerBuilder.RegisterType<amalgamators.HeaderAmalgamator>().As<IAmalgamator<ilrModelRw.MessageHeader>>();
            containerBuilder.RegisterType<amalgamators.HeaderSourceAmalgamator>().As<IAmalgamator<ilrModelRw.MessageHeaderSource>>();
            containerBuilder.RegisterType<amalgamators.HeaderCollectionDetailsAmalgamator>().As<IAmalgamator<ilrModelRw.MessageHeaderCollectionDetails>>();
            containerBuilder.RegisterType<amalgamators.LearnerAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearner>>();
            containerBuilder.RegisterType<amalgamators.LearnerDestinationandProgressionAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerDestinationandProgression>>();

            // LearnerAmalgamator dependencies
            containerBuilder.RegisterType<amalgamators.LLDDandHealthProblemAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerLLDDandHealthProblem>>();
            containerBuilder.RegisterType<amalgamators.ProviderSpecLearnerMonitoringAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerProviderSpecLearnerMonitoring>>();
            containerBuilder.RegisterType<amalgamators.LearnerEmploymentStatusAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerLearnerEmploymentStatus>>();
            containerBuilder.RegisterType<amalgamators.LearnerHEAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerLearnerHE>>();

            // LearnerHEAmalgamator dependencies
            containerBuilder.RegisterType<amalgamators.LearnerHEFinancialSupportAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerLearnerHELearnerHEFinancialSupport>>();

            containerBuilder.RegisterType<amalgamators.LearnerEmploymentStatusMonitoringAmalgamator>().As<IAmalgamator<ilrModelRw.MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring>>();
        }
    }
}