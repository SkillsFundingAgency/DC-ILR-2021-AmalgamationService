using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseSource
    {
        string ProtectiveMarkingString { get;  set; }

        int UKPRN { get;  set; }

        string SoftwareSupplier { get;  set; }

        string SoftwarePackage { get;  set; }

        string Release { get;  set; }

        string SerialNo { get;  set; }

        DateTime DateTime { get;  set; }

        string ReferenceData { get;  set; }

        string ComponentSetVersion { get;  set; }
    }
}
