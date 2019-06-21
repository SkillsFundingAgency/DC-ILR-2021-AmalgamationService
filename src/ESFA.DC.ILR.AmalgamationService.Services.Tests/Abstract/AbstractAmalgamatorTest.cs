using ESFA.DC.ILR.AmalgamationService.Interfaces;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract
{
    public class AbstractAmalgamatorTest : BaseTest
    {
        private Mock<IAmalgamator<TestData>> _mockTestDataAmalgamatos = new Mock<IAmalgamator<TestData>>();

        private Mock<IRule<string>> _mockRule = new Mock<IRule<string>>();
        private AbstractAmalgamatorCaller _amalgamator = new AbstractAmalgamatorCaller();
        private List<TestData> _testDatas = new List<TestData>();

        public AbstractAmalgamatorTest()
        {
            _mockTestDataAmalgamatos.Setup(m => m.Amalgamate(It.IsAny<List<TestData>>())).Returns((List<TestData> s) => s[0]);

            _mockRule.Setup(m => m.Definition(It.IsAny<List<string>>())).Returns((List<string> s) => s[0]);

            _testDatas.Add(GetTestData("11!", "Property11", 234234));
        }

        [Fact]
        public void ApplyRuleTest_Success()
        {
            TestData testDataAmalgamated = new TestData();

            _amalgamator.ApplyRuleCaller(d => d.PropertyStr, _mockRule.Object.Definition, _testDatas, testDataAmalgamated);
            Assert.Equal(testDataAmalgamated.PropertyStr, _testDatas[0].PropertyStr);
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

        private TestData GetTestData(string key, string propertyStr = "", long propertyLng = 0, TestData[] data = null)
        {
            return new TestData() { Key = key, PropertyStr = propertyStr, PropertyLng = propertyLng, Messages = data };
        }
    }
}