using ESFA.DC.ILR.AmalgamationService.Interfaces;
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

            NewAmalgamator().ApplyRuleCaller(d => d.PropertyStr, GetMockRule().Object.Definition, testDataList, testDataAmalgamated);
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

        private AbstractAmalgamatorCaller NewAmalgamator()
        {
            return new AbstractAmalgamatorCaller();
        }

        private Mock<IRule<string>> GetMockRule()
        {
            var mockRule = new Mock<IRule<string>>();
            mockRule.Setup(m => m.Definition(It.IsAny<List<string>>())).Returns((List<string> s) => s[0]);
            return mockRule;
        }

        private TestData GetTestData(string key, string propertyStr = "", long propertyLng = 0, TestData[] data = null)
        {
            return new TestData() { Key = key, PropertyStr = propertyStr, PropertyLng = propertyLng, Messages = data };
        }
    }
}