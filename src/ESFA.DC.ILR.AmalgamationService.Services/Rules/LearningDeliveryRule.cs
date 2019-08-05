﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;
using ESFA.DC.ILR.Model.Loose.ReadWrite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class LearningDeliveryRule : IRule<MessageLearnerLearningDelivery[]>
    {
        public IRuleResult<MessageLearnerLearningDelivery[]> Definition(IEnumerable<MessageLearnerLearningDelivery[]> learningDeliveries)
        {
            var flattenedLearningDeliveries = learningDeliveries.SelectMany(x => x);
            int aimSeqNumber = 0;
            foreach (var learningDelivery in flattenedLearningDeliveries)
            {
                learningDelivery.AimSeqNumber = ++aimSeqNumber;
            }

            return new RuleResult<MessageLearnerLearningDelivery[]> { AmalgamatedValue = flattenedLearningDeliveries.ToArray(), AmalgamationValidationErrors = null };
        }
    }
}
