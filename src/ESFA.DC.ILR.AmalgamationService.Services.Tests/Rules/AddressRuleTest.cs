using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class AddressRuleTest
    {
        [Theory]
        [InlineData("line 1", "line 2", "line 3", "line 4", "line 1", "line 2", "line 3", "line 4")]
        public void AlwaysTrue(string address11, string address12, string address13, string address14, string address21, string address22, string address23, string address24)
        {
            MessageLearner messageLearner1 = GetMessageLearner(address11, address12, address13, address14, "file1.xml", "learner 1");
            MessageLearner messageLearner2 = GetMessageLearner(address21, address22, address23, address24, "file2.xml", "learner 2");
            AddressRule addressRule = new AddressRule();
            List<MessageLearner> adresses = new List<MessageLearner>() { messageLearner1, messageLearner2 };
            var result = addressRule.Definition(adresses);
            result.AmalgamatedValue.AddLine1.Should().BeEquivalentTo("line 1");
            result.AmalgamatedValue.AddLine2.Should().BeEquivalentTo("line 2");
            result.AmalgamatedValue.AddLine3.Should().BeEquivalentTo("line 3");
            result.AmalgamatedValue.AddLine4.Should().BeEquivalentTo("line 4");
            result.Success.Equals(true);
            result.AmalgamationValidationErrors.Count().Equals(0);
        }

        [Theory]
        [InlineData("line 1", "line 2", "line 3", "line 4", "Line 1", "line 2", "line 3", "line 4")]
        [InlineData("Line 1", "LINE 2", "line 3", "line 4", "Line 1", "line 2", "lINE 3", "line 4")]
        [InlineData("line 1", "line 2", "line 3", "line 4", "Lin   e 1", "line-2", "line.3", "line,4")]
        [InlineData(null, "xxline 2", "87line 3", "mline 4", "line 1", "line 2", "line 3", "line 4")]
        public void AlwaysTrue0(string address11, string address12, string address13, string address14, string address21, string address22, string address23, string address24)
        {
            MessageLearner messageLearner1 = GetMessageLearner(address11, address12, address13, address14, "file1.xml", "learner 1");
            MessageLearner messageLearner2 = GetMessageLearner(address21, address22, address23, address24, "file2.xml", "learner 2");
            AddressRule addressRule = new AddressRule();
            List<MessageLearner> adresses = new List<MessageLearner>() { messageLearner1, messageLearner2 };
            var result = addressRule.Definition(adresses);
            result.AmalgamatedValue.AddLine1.Should().BeEquivalentTo("line 1");
            result.AmalgamatedValue.AddLine2.Should().BeEquivalentTo("line 2");
            result.AmalgamatedValue.AddLine3.Should().BeEquivalentTo("line 3");
            result.AmalgamatedValue.AddLine4.Should().BeEquivalentTo("line 4");
            result.Success.Equals(false);
            result.AmalgamationValidationErrors.Count().Equals(2);
        }

        [Theory]
        [InlineData("line 1", "", "line 3", "", "Line 1", "line 2", "line 3", "line 4")]
        public void AlwaysTrue1(string address11, string address12, string address13, string address14, string address21, string address22, string address23, string address24)
        {
            MessageLearner messageLearner1 = GetMessageLearner(address11, address12, address13, address14, "file1.xml", "learner 1");
            MessageLearner messageLearner2 = GetMessageLearner(address21, address22, address23, address24, "file2.xml", "learner 2");
            AddressRule addressRule = new AddressRule();
            List<MessageLearner> adresses = new List<MessageLearner>() { messageLearner1, messageLearner2 };
            var result = addressRule.Definition(adresses);
            result.AmalgamatedValue.AddLine1.Should().BeEquivalentTo("line 1");
            result.AmalgamatedValue.AddLine2.Should().BeEquivalentTo(string.Empty);
            result.AmalgamatedValue.AddLine3.Should().BeEquivalentTo("line 3");
            result.AmalgamatedValue.AddLine4.Should().BeEquivalentTo(string.Empty);
            result.Success.Equals(false);
            result.AmalgamationValidationErrors.Count().Equals(6);
        }

        [Theory]
        [InlineData(null, "line 2", "line 3", "line 4", "Line 1", null, "", null)]
        public void AlwaysTrue2(string address11, string address12, string address13, string address14, string address21, string address22, string address23, string address24)
        {
            MessageLearner messageLearner1 = GetMessageLearner(address11, address12, address13, address14, "file1.xml", "learner 1");
            MessageLearner messageLearner2 = GetMessageLearner(address21, address22, address23, address24, "file2.xml", "learner 2");
            AddressRule addressRule = new AddressRule();
            List<MessageLearner> adresses = new List<MessageLearner>() { messageLearner1, messageLearner2 };
            var result = addressRule.Definition(adresses);
            result.AmalgamatedValue.AddLine1.Should().BeEquivalentTo("Line 1");
            result.AmalgamatedValue.AddLine2.Should().BeEquivalentTo(null);
            result.AmalgamatedValue.AddLine3.Should().BeEquivalentTo(string.Empty);
            result.AmalgamatedValue.AddLine4.Should().BeEquivalentTo(null);
            result.Success.Equals(false);
            result.AmalgamationValidationErrors.Count().Equals(8);
        }

        private MessageLearner GetMessageLearner(string address1, string address2, string address3, string address4, string fileName, string learnRefNumber)
        {
            return new MessageLearner()
            {
                LearnRefNumber = learnRefNumber,
                AddLine1 = address1,
                AddLine2 = address2,
                AddLine3 = address3,
                AddLine4 = address4,
                Parent = new Message() { Parent = new AmalgamationRoot() { Filename = fileName } },
            };
        }
    }
}
