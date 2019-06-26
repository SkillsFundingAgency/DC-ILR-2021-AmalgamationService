using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite.Interface
{
    public interface ILooseLearnerEmploymentStatus
    {
        string AgreeId { get;  set; }

        long? EmpStatNullable { get;  set; }

        long? EmpIdNullable { get;  set; }

        DateTime? DateEmpStatAppNullable { get;  set; }

        IReadOnlyCollection<ILooseEmploymentStatusMonitoring> EmploymentStatusMonitorings { get;  set; }
    }
}
