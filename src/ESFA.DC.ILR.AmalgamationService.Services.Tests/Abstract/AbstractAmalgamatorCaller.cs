using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract
{
    public class AbstractAmalgamatorCaller : AbstractAmalgamator
    {
        public AbstractAmalgamatorCaller()
            : base(Enum.GetName(typeof(Entity), Entity.LearnerEmploymentStatus), string.Empty)
        {
        }

        public T ApplyRuleCaller<T, TValue>(Expression<Func<T, TValue>> selector, Func<IEnumerable<TValue>, IRuleResult<TValue>> rule, IEnumerable<T> inputEntities, T entity)
        {
            return ApplyRule(selector, rule, inputEntities, entity);
        }

        public T ApplyChildRuleCaller<T, TValue>(Expression<Func<T, TValue>> selector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
        {
            return ApplyChildRule(selector, amalgamator, inputEntities, entity);
        }

        public T ApplyGroupedChildCollectionRuleCaller<T, TValue, TGroupBy>(Expression<Func<T, IEnumerable<TValue>>> selector, Expression<Func<TValue, TGroupBy>> groupBySelector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
        {
            return ApplyGroupedChildCollectionRule(selector, groupBySelector, amalgamator, inputEntities, entity);
        }
    }
}
