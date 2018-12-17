using System.Linq.Expressions;

namespace RulesEngine.Entities
{
    public class Rule
    {
        public string ComparisonPredicate { get; set; }
        public ExpressionType? ComparisonOperator { get; set; }
        public string ComparisonValue { get; set; }
        public string CustomComparer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        /// <param name="comparisonPredicate">The comparison predicate/property name.</param>
        /// <param name="comparisonOperator">The comparison operator.</param>
        /// <param name="comparisonValue">The comparison value to evaluate.</param>
        /// <param name="customComparer">The custom comparer if comparison operator is null. String value of valid Linq expression.</param>
        public Rule(string comparisonPredicate, ExpressionType? comparisonOperator, string comparisonValue, string customComparer = null)
        {
            ComparisonPredicate = comparisonPredicate;
            ComparisonOperator = comparisonOperator;
            ComparisonValue = comparisonValue;
            CustomComparer = customComparer;
        }
    }
}
