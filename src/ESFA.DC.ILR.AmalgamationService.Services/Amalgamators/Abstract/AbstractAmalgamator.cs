using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Enum;
using ESFA.DC.ILR.Model.Loose.ReadWrite.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services.Amalgamators.Abstract
{
    public class AbstractAmalgamator<T>
        where T : IAmalgamationModel
    {
        private readonly Entity _entityType;
        private readonly Func<T, string> _keyValueSelectorFunc;

        protected AbstractAmalgamator(Entity entityType, Expression<Func<T, string>> keyValueSelector)
        {
            _entityType = entityType;
            _keyValueSelectorFunc = keyValueSelector.Compile();
        }

        protected List<IAmalgamationValidationError> AmalgamationValidationErrors { get; } = new List<IAmalgamationValidationError>();

        protected T ApplyRule<TValue>(Expression<Func<T, TValue>> selector, Func<IEnumerable<TValue>, IRuleResult<TValue>> rule, IEnumerable<T> inputEntities, T entity)
        {
            var selectorFunc = selector.Compile();

            var inputValues = inputEntities.Where(e => e != null).Select(e => selectorFunc.Invoke(e)).ToList();

            var value = rule.Invoke(inputValues);
            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;

            if (value.Success)
            {
                prop.SetValue(entity, value.Result);
            }
            else
            {
                inputEntities.ToList().ForEach(x => AmalgamationValidationErrors.Add(new AmalgamationValidationError()
                {
                    File = x.SourceFileName,
                    LearnRefNumber = x.LearnRefNumber,
                    Entity = Enum.GetName(typeof(Entity), _entityType),
                    Key = _keyValueSelectorFunc(x),

                    Value = prop.GetValue(x).ToString(),
                    ConflictingAttribute = prop.Name
                }));
            }

            return entity;
        }

        protected T ApplyChildRule<TValue>(Expression<Func<T, TValue>> selector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
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

        protected T ApplyGroupedChildCollectionRule<TValue, TGroupBy>(Expression<Func<T, IEnumerable<TValue>>> selector, Expression<Func<TValue, TGroupBy>> groupBySelector, IAmalgamator<TValue> amalgamator, IEnumerable<T> inputEntities, T entity)
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
