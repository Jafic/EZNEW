﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using EZNEW.ExpressionUtil;

namespace EZNEW.Develop.CQuery
{
    public static class NotNullExtensions
    {
        /// <summary>
        /// Field is not null
        /// </summary>
        /// <param name="sourceQuery">Source query</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="or">Connect with 'and'(true/default) or 'or'(false)</param>
        /// <returns>Return the newest IQuery object</returns>
        public static IQuery NotNull(this IQuery sourceQuery, string fieldName, bool or = false)
        {
            return sourceQuery.AddCriteria(or ? QueryOperator.OR : QueryOperator.AND, fieldName, CriteriaOperator.NotNull, null);
        }

        /// <summary>
        /// Field is not null
        /// </summary>
        /// <typeparam name="TQueryModel">Query model</typeparam>
        /// <param name="sourceQuery">Source query</param>
        /// <param name="field">Field</param>
        /// <param name="or">Connect with 'and'(true/default) or 'or'(false)</param>
        /// <returns>Return the newest IQuery object</returns>
        public static IQuery NotNull<TQueryModel>(this IQuery sourceQuery, Expression<Func<TQueryModel, dynamic>> field, bool or = false) where TQueryModel : IQueryModel<TQueryModel>
        {
            return sourceQuery.AddCriteria(or ? QueryOperator.OR : QueryOperator.AND, ExpressionHelper.GetExpressionPropertyName(field.Body), CriteriaOperator.NotNull, null);
        }
    }
}
