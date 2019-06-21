using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ESFA.DC.ILR.AmalgamationService.Services.Tests
{
    public class BaseTest
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

        public TValue TestAmalgamatorSuccess<T, TValue>(Expression<Func<T, TValue>> propertySelector, TValue value, Func<T> constructor, IAmalgamator<T> amalgamator)
        {
            var selectorFunc = propertySelector.Compile();

            var inputModels = GetInputModels(propertySelector, value, constructor);
            var amalgamatedModel = amalgamator.Amalgamate(inputModels);
            var result = amalgamatedModel.GetType().GetProperty(((MemberExpression)propertySelector.Body).Member.Name).GetValue(amalgamatedModel);

            return (TValue)result;
        }
    }
}
