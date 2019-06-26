using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseSourceFile
    {
        string SourceFileName { get;  set; }

        DateTime FilePreparationDate { get;  set; }

        string SoftwareSupplier { get;  set; }

        string SoftwarePackage { get;  set; }

        string Release { get;  set; }

        string SerialNo { get;  set; }

        DateTime? DateTimeNullable { get;  set; }
    }
}
