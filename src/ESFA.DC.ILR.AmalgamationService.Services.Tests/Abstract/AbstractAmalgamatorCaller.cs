using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract
{
    public class AbstractAmalgamatorCaller<T> : AbstractAmalgamator<T>
        where T : IAmalgamationModel
    {
        public AbstractAmalgamatorCaller(Entity entityType)
            : base(entityType, (x) => string.Empty)
        {
        }

        public T ApplyRuleCaller<TValue>(Expression<Func<T, TValue>> selector, Func<IEnumerable<TValue>, IRuleResult<TValue>> rule, IEnumerable<T> inputEntities, T entity)
        {
            return ApplyRule(selector, rule, inputEntities, entity);
        }

        public T ApplyChildRuleCaller<TValue>(Expression<Func<T, TValue>> selector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
        {
            return ApplyChildRule(selector, amalgamator, inputEntities, entity);
        }

        public T ApplyGroupedChildCollectionRuleCaller<TValue, TGroupBy>(Expression<Func<T, IEnumerable<TValue>>> selector, Expression<Func<TValue, TGroupBy>> groupBySelector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
        {
            return ApplyGroupedChildCollectionRule(selector, groupBySelector, amalgamator, inputEntities, entity);
        }
    }
}
