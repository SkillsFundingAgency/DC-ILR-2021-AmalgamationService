using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Schema.Interface
{
    public interface ISchemaProvider
    {
        XmlSchema Provide();
    }
}
