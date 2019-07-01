using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class TestData : IParentRelationship<ILooseMessage>, IAmalgamationModel
    {
        public string Key { get; set; }

        public string PropertyStr { get; set; }

        public long PropertyLng { get; set; }

        public TestData[] Messages { get; set; }

        public string SourceFileName => throw new NotImplementedException();

        public string LearnRefNumber => throw new NotImplementedException();

        public ILooseMessage Parent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
