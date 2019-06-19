using ESFA.DC.ILR.AmalgamationService.Interfaces;
using ESFA.DC.ILR.AmalgamationService.Services.Amalgamators;
using ESFA.DC.ILR.Model.Loose;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests.Abstract
{
    public class AbstractAmalgamatorTest
    {
        public IList<T> GetInputModels<T, TValue>(Expression<Func<T, TValue>> propertySelector, TValue value, Func<T> constructor)
        {
            return new List<T>()
            {
                GetInputModel(propertySelector, value, constructor),
                GetInputModel(propertySelector, value, constructor),
                GetInputModel(propertySelector, value, constructor),
            };
        }

        public T GetInputModel<T, TValue>(Expression<Func<T, TValue>> propertySelector, TValue value, Func<T> constructor)
        {
            var selectorFunc = propertySelector.Compile();

            T model = constructor();

            var prop = (PropertyInfo)((MemberExpression)propertySelector.Body).Member;
            prop.SetValue(model, value);

            return model;
        }
    }
}
