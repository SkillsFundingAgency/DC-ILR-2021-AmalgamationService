using Autofac;
using ESFA.DC.Serialization.Interfaces;
using ESFA.DC.Serialization.Xml;

namespace ESFA.DC.ILR.AmalgamationService.Modules
{
    public class SerializationModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<XmlSerializationService>().As<IXmlSerializationService>();
        }
    }
}