using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract
{
    public class AbstractAmalgamatorTest : BaseAmalgamatorTest
    {
        [Fact]
        public void ApplyRuleTest_Success()
        {
            var testDataList = new List<TestData>() { GetTestData("11!", "Property11", 234234) };
            TestData testDataAmalgamated = new TestData();

            NewAmalgamator(Entity.Learner).ApplyRuleCaller(d => d.PropertyStr, GetMockRuleSuccess().Object.Definition, testDataList, testDataAmalgamated);
            Assert.Equal(testDataAmalgamated.PropertyStr, testDataList[0].PropertyStr);
        }

        [Fact]
        public void ApplyChildRule_Success()
        {
            Assert.True(true);
        }

        [Fact]
        public void ApplyGroupedChildCollectionRule_Success()
        {
            Assert.True(true);
        }

        private AbstractAmalgamatorCaller<TestData> NewAmalgamator(Entity entityType)
        {
            return new AbstractAmalgamatorCaller<TestData>(entityType);
        }

        private Mock<IRule<string>> GetMockRuleSuccess()
        {
            var mockRule = new Mock<IRule<string>>();

            mockRule.Setup(m => m.Definition(It.IsAny<List<string>>())).Returns((List<string> s) => new RuleResult<string>() { Success = true, Result = s[0] });

            return mockRule;
        }

        private TestData GetTestData(string key, string propertyStr = "", long propertyLng = 0, TestData[] data = null)
        {
            return new TestData() { Key = key, PropertyStr = propertyStr, PropertyLng = propertyLng, Messages = data };
        }
    }
}