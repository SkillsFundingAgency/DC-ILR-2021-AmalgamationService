using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface IAmalgamationRoot
    {
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        string Filename { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        ILooseMessage Message { get; set; }
    }
}
