using ESFA.DC.ILR.Model.Loose.ReadWrite;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class TestData : IParentRelationship<Message>, IAmalgamationModel
    {
        public string Key { get; set; }

        public string PropertyStr { get; set; }

        public long PropertyLng { get; set; }

        public TestData[] Messages { get; set; }

        public string SourceFileName => throw new NotImplementedException();

        public string LearnRefNumber => throw new NotImplementedException();

        public Message Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public object ParentSetter { set => throw new NotImplementedException(); }
    }
}
