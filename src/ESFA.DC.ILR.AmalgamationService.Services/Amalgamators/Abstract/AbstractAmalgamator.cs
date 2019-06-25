using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract
{
    public class AbstractAmalgamator
    {
        protected IRuleContext RuleContext { get; set; }

        protected T ApplyRule<T, TValue>(Expression<Func<T, TValue>> selector, Func<IEnumerable<TValue>, TValue> rule, IEnumerable<T> inputEntities, T entity)
        {
            var selectorFunc = selector.Compile();

            var inputValues = inputEntities.Where(e => e != null).Select(e => selectorFunc.Invoke(e)).ToList();

            var value = rule.Invoke(inputValues);

            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
            prop.SetValue(entity, value);

            return entity;
        }

        protected T ApplyChildRule<T, TValue>(Expression<Func<T, TValue>> selector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
        {
            var selectorFunc = selector.Compile();

            var inputValues = inputEntities.Where(e => e != null).Select(e => selectorFunc.Invoke(e)).ToList();

            if (inputValues.Any())
            {
                var value = amalgamator.Amalgamate(inputValues);

                var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
                prop.SetValue(entity, value);
            }

            return entity;
        }

        protected T ApplyGroupedChildCollectionRule<T, TValue, TGroupBy>(Expression<Func<T, IEnumerable<TValue>>> selector, Expression<Func<TValue, TGroupBy>> groupBySelector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
        {
            if (inputEntities == null || inputEntities.Count() < 1)
            {
                return default(T);
            }

            var selectorFunc = selector.Compile();
            var groupByFunc = groupBySelector.Compile();

            if (!inputEntities.Any(e => e != null && selectorFunc.Invoke(e) != null))
            {
                return default(T);
            }

            var inputCollection = inputEntities.Where(e => e != null).SelectMany(selectorFunc);

            var inputGroups = inputCollection.GroupBy(groupByFunc);
            var amalgamatedGroups = inputGroups.Select(amalgamator.Amalgamate);

            if (amalgamatedGroups.Any())
            {
                var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
                prop.SetValue(entity, amalgamatedGroups.ToArray<TValue>());
            }

            return entity;
        }
    }
}
