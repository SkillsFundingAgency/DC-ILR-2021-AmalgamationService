using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearningDeliveryLearningDeliveryHE : AbstractLooseReadWriteModel<ILooseLearningDelivery>, ILooseLearningDeliveryHE
    {
        public long? TYPEYRNullable
        {
            get => tYPEYRFieldSpecified ? tYPEYRField : default(long?);
            set
            {
                tYPEYRFieldSpecified = value.HasValue;
                tYPEYRField = value.GetValueOrDefault();
            }
        }

        public long? MODESTUDNullable
        {
            get => mODESTUDFieldSpecified ? mODESTUDField : default(long?);
            set
            {
                mODESTUDFieldSpecified = value.HasValue;
                mODESTUDField = value.GetValueOrDefault();
            }
        }

        public long? FUNDLEVNullable
        {
            get => fUNDLEVFieldSpecified ? fUNDLEVField : default(long?);
            set
            {
                fUNDLEVFieldSpecified = value.HasValue;
                fUNDLEVField = value.GetValueOrDefault();
            }
        }

        public long? FUNDCOMPNullable
        {
            get => fUNDCOMPFieldSpecified ? fUNDCOMPField : default(long?);
            set
            {
                fUNDCOMPFieldSpecified = value.HasValue;
                fUNDCOMPField = value.GetValueOrDefault();
            }
        }

        public long? YEARSTUNullable
        {
            get => yEARSTUFieldSpecified ? yEARSTUField : default(long?);
            set
            {
                yEARSTUFieldSpecified = value.HasValue;
                yEARSTUField = value.GetValueOrDefault();
            }
        }

        public long? MSTUFEENullable
        {
            get => mSTUFEEFieldSpecified ? mSTUFEEField : default(long?);
            set
            {
                mSTUFEEFieldSpecified = value.HasValue;
                mSTUFEEField = value.GetValueOrDefault();
            }
        }

        public long? SPECFEENullable
        {
            get => sPECFEEFieldSpecified ? sPECFEEField : default(long?);
            set
            {
                sPECFEEFieldSpecified = value.HasValue;
                sPECFEEField = value.GetValueOrDefault();
            }
        }

        public long? SOC2000Nullable
        {
            get => sOC2000FieldSpecified ? sOC2000Field : default(long?);
            set
            {
                sOC2000FieldSpecified = value.HasValue;
                sOC2000Field = value.GetValueOrDefault();
            }
        }

        public long? SECNullable
        {
            get => sECFieldSpecified ? sECField : default(long?);
            set
            {
                sECFieldSpecified = value.HasValue;
                sECField = value.GetValueOrDefault();
            }
        }

        public long? NETFEENullable
        {
            get => nETFEEFieldSpecified ? nETFEEField : default(long?);
            set
            {
                nETFEEFieldSpecified = value.HasValue;
                nETFEEField = value.GetValueOrDefault();
            }
        }

        public long? GROSSFEENullable
        {
            get => gROSSFEEFieldSpecified ? gROSSFEEField : default(long?);
            set
            {
                gROSSFEEFieldSpecified = value.HasValue;
                gROSSFEEField = value.GetValueOrDefault();
            }
        }

        public long? ELQNullable
        {
            get => eLQFieldSpecified ? eLQField : default(long?);
            set
            {
                eLQFieldSpecified = value.HasValue;
                eLQField = value.GetValueOrDefault();
            }
        }

        public decimal? STULOADNullable
        {
            get => sTULOADFieldSpecified ? sTULOADField : default(decimal?);
            set
            {
                sTULOADFieldSpecified = value.HasValue;
                sTULOADField = value.GetValueOrDefault();
            }
        }

        public decimal? PCOLABNullable
        {
            get => pCOLABFieldSpecified ? pCOLABField : default(decimal?);
            set
            {
                pCOLABFieldSpecified = value.HasValue;
                pCOLABField = value.GetValueOrDefault();
            }
        }

        public decimal? PCFLDCSNullable
        {
            get => pCFLDCSFieldSpecified ? pCFLDCSField : default(decimal?);
            set
            {
                pCFLDCSFieldSpecified = value.HasValue;
                pCFLDCSField = value.GetValueOrDefault();
            }
        }

        public decimal? PCSLDCSNullable
        {
            get => pCSLDCSFieldSpecified ? pCSLDCSField : default(decimal?);
            set
            {
                pCSLDCSFieldSpecified = value.HasValue;
                pCSLDCSField = value.GetValueOrDefault();
            }
        }

        public decimal? PCTLDCSNullable
        {
            get => pCTLDCSFieldSpecified ? pCTLDCSField : default(decimal?);
            set
            {
                pCTLDCSFieldSpecified = value.HasValue;
                pCTLDCSField = value.GetValueOrDefault();
            }
        }
        
        public string SourceFileName => Parent.Parent.Parent.Parent.Filename;

        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
