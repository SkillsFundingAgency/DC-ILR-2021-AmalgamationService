﻿using System;
using System.Xml.Serialization;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Extension;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;

namespace ESFA.DC.ILR.Model.Loose.ReadWrite
{
    public partial class MessageHeaderSource : AbstractLooseReadWriteModel<ILooseHeader>, ILooseSource
    {
        [XmlIgnore]
        public string ProtectiveMarkingString
        {
            get => protectiveMarkingField.XmlEnumToString();
            set => protectiveMarkingField = (MessageHeaderSourceProtectiveMarking)Enum.Parse(typeof(MessageHeaderSourceProtectiveMarking), value);
        }

        [XmlIgnore]
        public string SourceFileName { get => Parent.Parent.Parent.Filename; }

        [XmlIgnore]
        public string LearnRefNumber => null;
    }
}
