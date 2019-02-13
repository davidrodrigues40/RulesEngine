using RuleEngine.Extensions;
using RuleEngineTester.Entities;
using RulesEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RuleEngineTester.Services
{
    public static class RuleCreatorService
    {
        /// <summary>
        /// Gets the car rules.
        /// </summary>
        /// <value>
        /// The car rules.
        /// </value>
        public static List<Func<ICar, bool>> CarExpressions { get; private set; }

        public static List<Rule> CarRules { get; private set; }

        /// <summary>
        /// Gets the person rules.
        /// </summary>
        /// <value>
        /// The person rules.
        /// </value>
        public static List<Func<IPerson, bool>> PersonExpressions { get; private set; }

        public static List<Rule> PersonRules { get; private set; }

        /// <summary>
        /// Creates the rules.
        /// </summary>
        public static void CreateRules()
        {
            CompileCarRules();

            CompilePersonRules();
        }

        private static void CompileCarRules()
        {
            CarRules = new List<Rule>
            {
                new Rule("Year", ExpressionType.GreaterThan, "2012"),
                new Rule("Make", ExpressionType.Equal, "Ford"),
                new Rule("Style", ExpressionType.Equal, "Coupe")
            };

            CarExpressions = CarRules.GenerateRules<ICar>();
        }

        private static void CompilePersonRules()
        {

            PersonRules = new List<Rule>
            {
                new Rule("FirstName", null, "S", "StartsWith")
            };

            PersonExpressions = PersonRules.GenerateRules<IPerson>();
        }
    }
}
