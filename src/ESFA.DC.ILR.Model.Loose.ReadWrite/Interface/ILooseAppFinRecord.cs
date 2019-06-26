using System;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseAppFinRecord
    {
        string AFinType { get;  set; }
        
        long? AFinCodeNullable { get;  set; }

        DateTime? AFinDateNullable { get;  set; }

        long? AFinAmountNullable { get;  set; }
    }
}
