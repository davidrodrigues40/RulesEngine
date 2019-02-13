using RulesEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RulesEngine
{
    public class RuleCompiler
    {
        /// <summary>
        /// Compiles the rules.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rules">The rules.</param>
        /// <returns></returns>
        public static List<Func<T, bool>> CompileRules<T>(List<Rule> rules)
        {
            var compiledRules = new List<Func<T, bool>>();

            // Loop through the rules and compile them against the properties of the supplied shallow object 
            rules.ForEach(rule =>
            {
                if (string.IsNullOrEmpty(rule.CustomComparer))
                    compiledRules.Add(CompileRule<T>(rule));
                else
                    compiledRules.Add(CompileCustomRule<T>(rule));
            });

            // Return the compiled rules to the caller

            return compiledRules;
        }

        private static Func<T, bool> CompileRule<T>(Rule rule)
        {
            try
            {
                // Get parameter expressions
                ParameterExpression genericType = Expression.Parameter(typeof(T));
                IEnumerable<ParameterExpression> expressions = new List<ParameterExpression> { genericType };

                // Get Key and property name from comparison predicate of rule
                MemberExpression key = MemberExpression.Property(genericType, rule.ComparisonPredicate);

                PropertyInfo property = typeof(T).GetProperty(rule.ComparisonPredicate);
                string propertyName = property.Name;

                // Get Property Type
                Type propertyType = property.PropertyType;

                // Convert the comparison value to the property type
                ConstantExpression value = Expression.Constant(Convert.ChangeType(rule.ComparisonValue, propertyType));

                // Create binary expression
                BinaryExpression binaryExpression = Expression.MakeBinary((ExpressionType)rule.ComparisonOperator, key, value);

                // Create lamba expression as Func using binary expression. Name of function is property name
                return Expression.Lambda<Func<T, bool>>(binaryExpression, propertyName, expressions).Compile();
            }
            catch (Exception e)
            {
                return ProcessException<T>(rule.ComparisonPredicate);
            }
        }

        private static Func<T, bool> ProcessException<T>(string propName)
        {
            Type type = typeof(T);
            ParameterExpression genericType = Expression.Parameter(type);
            NewExpression expression = Expression.New(typeof(bool));
            IEnumerable<ParameterExpression> expressions = new List<ParameterExpression> { genericType };

            return Expression.Lambda<Func<T, bool>>(expression, propName, expressions).Compile();
        }

        private static Func<T, bool> CompileCustomRule<T>(Rule rule)
        {
            try
            {
                ParameterExpression genericType = Expression.Parameter(typeof(T));
                IEnumerable<ParameterExpression> expressions = new List<ParameterExpression> { genericType };

                MemberExpression key = MemberExpression.Property(genericType, rule.ComparisonPredicate);
                PropertyInfo property = typeof(T).GetProperty(rule.ComparisonPredicate);
                string propertyName = property.Name;

                MethodInfo method = typeof(string).GetMethod(rule.CustomComparer, new[] { typeof(string) });
                var someValue = Expression.Constant(rule.ComparisonValue, typeof(string));
                var containsMethodExp = Expression.Call(key, method, someValue);

                return Expression.Lambda<Func<T, bool>>(containsMethodExp, propertyName, expressions).Compile();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
