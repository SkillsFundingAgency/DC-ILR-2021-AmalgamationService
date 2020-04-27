using ESFA.DC.ILR.AmalgamationService.Services.Rules;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Rules
{
    public class DPOutcomeRuleTest
    {
        [Fact]
        public void LlddCat20PassDuplicates()
        {
            DPOutcomeRule dPOutcomeRule = new DPOutcomeRule();

            DateTime outStartDate = new DateTime(2019, 10, 10);
            DateTime outEndDate = new DateTime(2019, 10, 10);
            DateTime outCollDate = new DateTime(2019, 10, 10);
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> firstList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate, outCollDate, "first.xml");
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> secondList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate, outCollDate, "second.xml");
            List<MessageLearnerDestinationandProgressionDPOutcome[]> items = new List<MessageLearnerDestinationandProgressionDPOutcome[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = dPOutcomeRule.Definition(items);

            result.AmalgamatedValue.Count().Should().Equals(1);
            result.Success.Should().Equals(true);
            result.AmalgamationValidationErrors.Count().Should().Equals(0);
        }

        [Fact]
        public void LlddCat23PassMultiple()
        {
            DPOutcomeRule dPOutcomeRule = new DPOutcomeRule();

            DateTime outStartDate = new DateTime(2019, 10, 10);
            DateTime outEndDate = new DateTime(2019, 10, 10);
            DateTime outCollDate = new DateTime(2019, 10, 10);
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> firstList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate, outCollDate, "first.xml");
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> secondList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 2", 2, outStartDate, outEndDate, outCollDate.AddDays(1), "second.xml");
            List<MessageLearnerDestinationandProgressionDPOutcome[]> items = new List<MessageLearnerDestinationandProgressionDPOutcome[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = dPOutcomeRule.Definition(items);

            result.AmalgamatedValue.Count().Should().Equals(2);
            result.Success.Should().Equals(true);
            result.AmalgamationValidationErrors.Count().Should().Equals(0);
        }

        [Fact]
        public void LlddCat23PassFailConflictEndDate()
        {
            DPOutcomeRule dPOutcomeRule = new DPOutcomeRule();

            DateTime outStartDate = new DateTime(2019, 10, 10);
            DateTime outEndDate = new DateTime(2019, 10, 10);
            DateTime outCollDate = new DateTime(2019, 10, 10);
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> firstList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate, outCollDate, "first.xml");
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> secondList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate.AddDays(1), outCollDate, "second.xml");
            List<MessageLearnerDestinationandProgressionDPOutcome[]> items = new List<MessageLearnerDestinationandProgressionDPOutcome[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = dPOutcomeRule.Definition(items);

            result.AmalgamatedValue.Count().Should().Equals(0);
            result.Success.Should().Equals(false);
            result.AmalgamationValidationErrors.Count().Should().Equals(2);
        }

        [Fact]
        public void LlddCat23PassFailConflictCollDate()
        {
            DPOutcomeRule dPOutcomeRule = new DPOutcomeRule();

            DateTime outStartDate = new DateTime(2019, 10, 10);
            DateTime outEndDate = new DateTime(2019, 10, 10);
            DateTime outCollDate = new DateTime(2019, 10, 10);
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> firstList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate, outCollDate, "first.xml");
            IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> secondList = GetMessageLearnerDestinationandProgressionDPOutcomes("Outcome 1", 1, outStartDate, outEndDate, outCollDate.AddDays(1), "second.xml");
            List<MessageLearnerDestinationandProgressionDPOutcome[]> items = new List<MessageLearnerDestinationandProgressionDPOutcome[]>() { firstList.ToArray(), secondList.ToArray() };
            var result = dPOutcomeRule.Definition(items);

            result.AmalgamatedValue.Count().Should().Equals(0);
            result.Success.Should().Equals(false);
            result.AmalgamationValidationErrors.Count().Should().Equals(2);
        }

        private IEnumerable<MessageLearnerDestinationandProgressionDPOutcome> GetMessageLearnerDestinationandProgressionDPOutcomes(string outType, long outCode, DateTime outStartDate, DateTime outEndDate, DateTime outCollDate, string filename)
        {
            return new List<MessageLearnerDestinationandProgressionDPOutcome>() { GetMessageLearnerDestinationandProgressionDPOutcome(outType, outCode, outStartDate, outEndDate, outCollDate, filename) };
        }

        private MessageLearnerDestinationandProgressionDPOutcome GetMessageLearnerDestinationandProgressionDPOutcome(string outType, long outCode, DateTime outStartDate, DateTime outEndDate, DateTime outCollDate, string filename)
        {
            return new MessageLearnerDestinationandProgressionDPOutcome() { OutType = outType, OutCodeNullable = outCode, OutCollDateNullable = outCollDate, OutEndDateNullable = outEndDate, OutStartDateNullable = outStartDate, Parent = GetMessageLearner(filename) };
        }

        private MessageLearnerDestinationandProgression GetMessageLearner(string filename)
        {
            return new MessageLearnerDestinationandProgression() { Parent = new Message() { Parent = new AmalgamationRoot() { Filename = filename } } };
        }
    }
}