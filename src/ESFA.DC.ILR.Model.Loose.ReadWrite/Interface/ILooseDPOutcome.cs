using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseDPOutcome
    {
        string OutType { get;  set; }

        long? OutCodeNullable { get;  set; }

        DateTime? OutStartDateNullable { get;  set; }

        DateTime? OutEndDateNullable { get;  set; }

        DateTime? OutCollDateNullable { get;  set; }
    }
}
