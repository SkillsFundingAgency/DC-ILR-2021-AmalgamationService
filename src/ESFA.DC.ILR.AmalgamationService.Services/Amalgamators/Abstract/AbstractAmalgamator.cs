using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ESFA.DC.ILR.AmalgamationService.Interfaces;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract
{
    public class AbstractAmalgamator
    {
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
    }
}
