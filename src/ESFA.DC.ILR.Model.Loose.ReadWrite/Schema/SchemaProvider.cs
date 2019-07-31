using ESFA.DC.ILR.Model.Loose.ReadWrite.Schema.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Schema
{
    public class SchemaProvider : ISchemaProvider
    {
        public XmlSchema Provide()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var xsdResourceName = assembly.GetManifestResourceNames().First(n => n.EndsWith(".xsd"));

            using (Stream xsdStream = assembly.GetManifestResourceStream(xsdResourceName))
            {
                using (var xmlReader = XmlReader.Create(xsdStream))
                {
                    return XmlSchema.Read(xmlReader, null);
                }
            }
        }
    }
}
