namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearningDeliveryHE
    {
        string NUMHUS { get;  set; }
        string SSN { get;  set; }
        string QUALENT3 { get;  set; }
        string UCASAPPID { get;  set; }
        string DOMICILE { get;  set; }
        string HEPostCode { get;  set; }

        long? TYPEYRNullable { get;  set; }
        long? MODESTUDNullable { get;  set; }
        long? FUNDLEVNullable { get;  set; }
        long? FUNDCOMPNullable { get;  set; }
        long? YEARSTUNullable { get;  set; }
        long? MSTUFEENullable { get;  set; }
        long? SPECFEENullable { get;  set; }
        long? SOC2000Nullable { get;  set; }
        long? SECNullable { get;  set; }
        long? NETFEENullable { get;  set; }
        long? GROSSFEENullable { get;  set; }
        long? ELQNullable { get;  set; }

        decimal? STULOADNullable { get;  set; }
        decimal? PCOLABNullable { get;  set; }
        decimal? PCFLDCSNullable { get;  set; }
        decimal? PCSLDCSNullable { get;  set; }
        decimal? PCTLDCSNullable { get;  set; }
    }   
}
