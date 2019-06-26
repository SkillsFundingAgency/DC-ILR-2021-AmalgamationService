using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearnerHE
    {
        string UCASPERID { get;  set; }

        long? TTACCOMNullable { get;  set; }

        IReadOnlyCollection<ILooseLearnerHEFinancialSupport> LearnerHEFinancialSupports { get;  set; }
    }
}
