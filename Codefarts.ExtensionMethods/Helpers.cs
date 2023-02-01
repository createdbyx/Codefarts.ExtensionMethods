// <copyright file="Helpers.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace System.ComponentModel
{
    using System.Linq.Expressions;
    using System.Reflection;

    internal class Helpers
    {
        /// <summary>
        /// Gets the member information for a given expression.
        /// </summary>
        /// <param name="expression">The expression to get the member info from.</param>
        /// <returns>A reference to a <see cref="MemberInfo"/> object.</returns>
        public static MemberInfo GetMemberInfo(Expression expression)
        {
            var lambda = (LambdaExpression)expression;

            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            return memberExpression.Member;
        }
    }
}