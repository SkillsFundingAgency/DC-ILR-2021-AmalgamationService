using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageLearnerLearnerEmploymentStatus : AbstractLooseReadWriteModel<ILooseLearner>, ILooseLearnerEmploymentStatus
    {
        [XmlIgnore]
        public long? EmpStatNullable
        {
            get => empStatFieldSpecified ? empStatField : default(long?);
            set
            {
                empStatFieldSpecified = value.HasValue;
                empStatField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public long? EmpIdNullable
        {
            get => empIdFieldSpecified ? empIdField : default(long?);
            set
            {
                empIdFieldSpecified = value.HasValue;
                empIdField = value.GetValueOrDefault();
            }
        }
        
        [XmlIgnore]
        public DateTime? DateEmpStatAppNullable
        {
            get => dateEmpStatAppFieldSpecified ? dateEmpStatAppField : default(DateTime?);
            set
            {
                dateEmpStatAppFieldSpecified = value.HasValue;
                dateEmpStatAppField = value.GetValueOrDefault();
            }
        }

        [XmlIgnore]
        public IReadOnlyCollection<ILooseEmploymentStatusMonitoring> EmploymentStatusMonitorings
        {
            get => employmentStatusMonitoringField;
            set => employmentStatusMonitoringField = (MessageLearnerLearnerEmploymentStatusEmploymentStatusMonitoring[])value;
        }

        [XmlIgnore]
        public string SourceFileName => Parent.Parent.Parent.Filename;

        [XmlIgnore]
        public string LearnRefNumber => Parent.LearnRefNumber;
    }
}
