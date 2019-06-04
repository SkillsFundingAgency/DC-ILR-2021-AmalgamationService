using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using ESFA.DC.ILR.Model.Loose.Schema;
using ESFA.DC.ILR.Model.Loose.Schema.Interface;
namespace ESFA.DC.ILR.AmalgamationService.Modules
{
    public class AmalgamationOrchestrationServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<IlrLooseXmlSchemaProvider>().As<IIlrLooseXmlSchemaProvider>();
        }
    }
}
