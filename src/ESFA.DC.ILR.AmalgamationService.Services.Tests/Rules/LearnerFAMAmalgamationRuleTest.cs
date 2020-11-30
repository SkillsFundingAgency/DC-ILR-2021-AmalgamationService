using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class LearnerFAMAmalgamationRuleTest
    {
        private readonly IDictionary<string, int> famTypesMaxOccurenceDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["HNS"] = 1,
            ["EHC"] = 1,
            ["DLA"] = 1,
            ["LSR"] = 4,
            ["SEN"] = 1,
            ["NLM"] = 2,
            ["EDF"] = 2,
            ["MCF"] = 1,
            ["ECF"] = 1,
            ["FME"] = 1,
        };

        [InlineData("HNS", 1)]
        [InlineData("EHC", 1)]
        [InlineData("DLA", 1)]
        [InlineData("LSR", 4)]
        [InlineData("SEN", 1)]
        [InlineData("NLM", 2)]
        [InlineData("EDF", 2)]
        [InlineData("MCF", 1)]
        [InlineData("ECF", 1)]
        [InlineData("FME", 1)]
        [Theory]
        public void TestEachFamtypeInIsolationPass(string famType, int occurrences)
        {
            var learnerFAMAmalgamationRule = new LearnerFAMAmalgamationRule();

            var famsOne = GetFam("ILR-99999999-2021-20200704-092701-05.xml", famType, occurrences);
            var famsTwo = GetFam("ILR-99999999-2021-20200704-092701-06.xml", famType, occurrences);

            IEnumerable<List<MessageLearnerLearnerFAM>> fams = new List<List<MessageLearnerLearnerFAM>>()
            {
                famsOne.ToList(),
                famsTwo.ToList(),
            };

            var result = learnerFAMAmalgamationRule.Definition(fams);

            result.AmalgamatedValue.Should().HaveCount(occurrences);

            result.AmalgamatedValue.Where(x => x.LearnFAMType.Equals(famType, StringComparison.OrdinalIgnoreCase)).Should().HaveCount(occurrences);
        }

        [InlineData("HNS", 1)]
        [InlineData("EHC", 1)]
        [InlineData("DLA", 1)]
        [InlineData("LSR", 4)]
        [InlineData("SEN", 1)]
        [InlineData("NLM", 2)]
        [InlineData("EDF", 2)]
        [InlineData("MCF", 1)]
        [InlineData("ECF", 1)]
        [InlineData("FME", 1)]
        [Theory]
        public void TestEachFamtypeInIsolationFail(string famType, int occurrences)
        {
            var learnerFAMAmalgamationRule = new LearnerFAMAmalgamationRule();

            var famsOne = GetFam("ILR-99999999-2021-20200704-092701-05.xml", famType, occurrences + 1);
            var famsTwo = GetFam("ILR-99999999-2021-20200704-092701-06.xml", famType, occurrences + 1);

            IEnumerable<List<MessageLearnerLearnerFAM>> fams = new List<List<MessageLearnerLearnerFAM>>()
            {
                famsOne.ToList(),
                famsTwo.ToList(),
            };

            var result = learnerFAMAmalgamationRule.Definition(fams);

            result.AmalgamatedValue.Should().HaveCount(0);
            result.AmalgamationValidationErrors.Should().HaveCount((occurrences + 1) * 2);
        }

        [InlineData("XFME", 1)]
        [InlineData("XPPE", 2)]
        [Theory]
        public void TestNotInListFail(string famType, int occurrences)
        {
            var learnerFAMAmalgamationRule = new LearnerFAMAmalgamationRule();

            var famsOne = GetFam("ILR-99999999-2021-20200704-092701-05.xml", famType, occurrences);
            var famsTwo = GetFam("ILR-99999999-2021-20200704-092701-06.xml", famType, occurrences);

            IEnumerable<List<MessageLearnerLearnerFAM>> fams = new List<List<MessageLearnerLearnerFAM>>()
            {
                famsOne,
                famsTwo,
            };

            var result = learnerFAMAmalgamationRule.Definition(fams);

            result.AmalgamatedValue.Should().HaveCount(0);
        }

        [Fact]
        public void AlwaysTrue()
        {
            var learnerFAMAmalgamationRule = new LearnerFAMAmalgamationRule();

            var famsOne = GetAllValidFams("ILR-99999999-2021-20200704-092701-05.xml");
            var famsTwo = GetAllValidFams("ILR-99999999-2021-20200704-092701-06.xml");

            IEnumerable<List<MessageLearnerLearnerFAM>> fams = new List<List<MessageLearnerLearnerFAM>>()
            {
                famsOne, famsTwo,
            };

            var result = learnerFAMAmalgamationRule.Definition(fams);

            result.AmalgamatedValue.Should().HaveCount(15);

            foreach (var famType in famTypesMaxOccurenceDictionary)
            {
                result.AmalgamatedValue.Where(x => x.LearnFAMType.Equals(famType.Key, StringComparison.OrdinalIgnoreCase)).Should().HaveCount(famType.Value);
            }
        }

        private List<MessageLearnerLearnerFAM> GetAllValidFams(string filename)
        {
            List<MessageLearnerLearnerFAM> fams = new List<MessageLearnerLearnerFAM>();

            var parent = GetMessageLearner(filename);

            foreach (var famType in famTypesMaxOccurenceDictionary)
            {
                for (var occurence = 0; occurence < famType.Value; occurence++)
                {
                    fams.Add(GetMessageLearnerLearnerFAM(famType.Key, occurence, parent));
                }
            }

            return fams.ToList();
        }

        private List<MessageLearnerLearnerFAM> GetFam(string filename, string famType, int occurences)
        {
            List<MessageLearnerLearnerFAM> fams = new List<MessageLearnerLearnerFAM>();

            var parent = GetMessageLearner(filename);

            var famCode = 0;

            for (var occurence = 0; occurence < occurences; occurence++)
            {
                fams.Add(GetMessageLearnerLearnerFAM(famType, famCode++, parent));
            }

            return fams.ToList();
        }

        private MessageLearnerLearnerFAM GetMessageLearnerLearnerFAM(string learnFAMType, long famCode, MessageLearner parent = null)
        {
            return new MessageLearnerLearnerFAM() { LearnFAMType = learnFAMType, LearnFAMCode = famCode, Parent = parent };
        }

        private MessageLearner GetMessageLearner(string filename)
        {
            return new MessageLearner() { Parent = new Message() { Parent = new AmalgamationRoot() { Filename = filename } } };
        }
    }
}