﻿using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Interfaces.Attribute;
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
        private readonly IAmalgamationErrorHandler _amalgamationErrorHandler;
        private readonly Func<T, string> _keyValueSelectorFunc;

        protected AbstractAmalgamator(Entity entityType, Expression<Func<T, string>> keyValueSelector, IAmalgamationErrorHandler amalgamationErrorHandler)
        {
            _entityType = entityType;
            _amalgamationErrorHandler = amalgamationErrorHandler;
            _keyValueSelectorFunc = keyValueSelector.Compile();
        }

        protected T ApplyRule<TValue>(Expression<Func<T, TValue>> selector, Func<IEnumerable<TValue>, IRuleResult<TValue>> rule, IEnumerable<T> inputEntities, T entity, Severity severity = Severity.Error)
        {
            var selectorFunc = selector.Compile();

            var inputValues = inputEntities.Where(e => e != null).Select(e => selectorFunc.Invoke(e)).Where(f => f != null).ToList();

            if (inputValues == null || inputValues.All(i => i == null))
            {
                return default(T);
            }

            var value = rule.Invoke(inputValues);
            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;

            if (value.Success)
            {
                prop.SetValue(entity, value.AmalgamatedValue);
            }
            else
            {
                _amalgamationErrorHandler.HandleErrors(inputEntities.Select(x => new AmalgamationValidationError()
                {
                    File = x.SourceFileName,
                    LearnRefNumber = x.LearnRefNumber ?? string.Empty,
                    Entity = Enum.GetName(typeof(Entity), _entityType),
                    Key = string.Format("{0} : {1}", GetKeyPropertyName(), _keyValueSelectorFunc(x)),
                    Value = prop.GetValue(x).ToString(),
                    ConflictingAttribute = prop.Name,
                    Severity = severity
                }));
            }

            return entity;
        }

        protected T ApplyGroupedCollectionRule<TValue, TGroupBy>(Expression<Func<T, TValue[]>> selector, Expression<Func<TValue, TGroupBy>> groupBySelector, Func<IEnumerable<TValue[]>, IRuleResult<TValue[]>> rule, IEnumerable<T> inputEntities, T entity, Entity entityType, Expression<Func<TValue, string>> keyValueSelector, Severity severity = Severity.Error)
            where TValue : IAmalgamationModel
        {
            if (inputEntities == null || !inputEntities.Any())
            {
                return default(T);
            }

            var selectorFunc = selector.Compile();
            var groupByFunc = groupBySelector.Compile();

            if (inputEntities.All(e => e == null || selectorFunc.Invoke(e) == null))
            {
                return default(T);
            }

            var inputCollection = inputEntities.Where(e => e != null).SelectMany(selectorFunc);

            var inputGroups = inputCollection.GroupBy(groupByFunc);
            var amalgamatedValues = new List<TValue>();

            var prop = (PropertyInfo)((MemberExpression)selector.Body).Member;
            var keyValueSelectorFunc = keyValueSelector.Compile();

            foreach (var groupValue in inputGroups)
            {
                if (groupValue == null || groupValue.All(i => i == null))
                {
                    return default(T);
                }

                var inputList = new List<TValue[]>() { groupValue.ToArray() };

                var amalgamationResult = rule.Invoke(inputList);

                if (amalgamationResult.Success)
                {
                    amalgamatedValues.AddRange(amalgamationResult.AmalgamatedValue);
                }
                else
                {
                    _amalgamationErrorHandler.HandleErrors(groupValue.Select(x => new AmalgamationValidationError()
                    {
                        File = x.SourceFileName,
                        LearnRefNumber = x.LearnRefNumber ?? string.Empty,
                        Entity = Enum.GetName(typeof(Entity), entityType),
                        Key = string.Format("{0} : {1}", GetKeyPropertyName(entityType), keyValueSelectorFunc(x)),
                        //Value = prop.GetValue(x).ToString(),
                        ConflictingAttribute = prop.Name,
                        Severity = severity
                    }));
                }
            }

            if (amalgamatedValues.Count > 0)
            {
                prop.SetValue(entity, amalgamatedValues.ToArray<TValue>());
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

        private string GetKeyPropertyName(Entity entityType)
        {
            var properties = entityType.GetAttribute<KeyProperty>();
            return properties == null ? string.Empty : properties.PropertyName;
        }

        private string GetKeyPropertyName()
        {
            return GetKeyPropertyName(_entityType);
        }
    }
}
