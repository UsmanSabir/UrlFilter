﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UrlFilter
{
    internal class ExpressionOperator
    {
        private static readonly Dictionary<string, Func<Expression, Expression, Expression>> Expressions = GetExpressions();
        public Expression OperatorExpression(string operation, Expression left, Expression right)
        {
            var lowerOperation = operation.Trim().ToLowerInvariant();
            Func<Expression, Expression, Expression> expression;
            if (Expressions.TryGetValue(lowerOperation, out expression))
            {
                return expression(left, right);
            }
            throw new QueryStringException($"Filter of type '{operation}' is not a valid query string operator");
        }

        private static Dictionary<string, Func<Expression, Expression, Expression>> GetExpressions()
        {
            return new Dictionary<string, Func<Expression, Expression, Expression>>
            {
                {"eq", Expression.Equal},
                {"ne", Expression.NotEqual},
                {"gt", Expression.GreaterThan},
                {"ge", Expression.GreaterThanOrEqual},
                {"lt", Expression.LessThan},
                {"le", Expression.LessThanOrEqual},
                {"or", Expression.OrElse},
                {"and", Expression.AndAlso}
            };
        }
    }
}