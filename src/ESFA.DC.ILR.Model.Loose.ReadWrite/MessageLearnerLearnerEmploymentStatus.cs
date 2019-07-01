using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerEmploymentStatus : ILooseLearnerEmploymentStatus
    {
        public long? EmpStatNullable
        {
            get => empStatFieldSpecified ? empStatField : default(long?);
            set
            {
                empStatFieldSpecified = value.HasValue;
                empStatField = value.GetValueOrDefault();
            }
        }

        public long? EmpIdNullable
        {
            get => empIdFieldSpecified ? empIdField : default(long?);
            set
            {
                empIdFieldSpecified = value.HasValue;
                empIdField = value.GetValueOrDefault();
            }
        }

        public DateTime? DateEmpStatAppNullable
        {
            get => dateEmpStatAppFieldSpecified ? dateEmpStatAppField : default(DateTime?);
            set
            {
                dateEmpStatAppFieldSpecified = value.HasValue;
                dateEmpStatAppField = value.GetValueOrDefault();
            }
        }

        public IReadOnlyCollection<ILooseEmploymentStatusMonitoring> EmploymentStatusMonitorings
        {
            get => employmentStatusMonitoringField;
            set => employmentStatusMonitoringField = (MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[])value;
        }
        public ILooseLearner Parent { get => Learner; set => Learner = value; }

        public string SourceFileName => Learner.Message.AmalgamationRoot.Filename;

        public string LearnRefNumber => Learner.LearnRefNumber;

        public ILooseLearner Learner { get; set; }
    }
}
