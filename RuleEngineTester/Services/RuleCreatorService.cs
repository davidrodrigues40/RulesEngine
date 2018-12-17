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
        public static List<Func<ICar, bool>> CarRules { get; private set; }

        /// <summary>
        /// Gets the person rules.
        /// </summary>
        /// <value>
        /// The person rules.
        /// </value>
        public static List<Func<IPerson, bool>> PersonRules { get; private set; }

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
            List<Rule> carRules = new List<Rule>
            {
                new Rule("Year", ExpressionType.GreaterThan, "2012"),
                new Rule("Make", ExpressionType.Equal, "Ford"),
                new Rule("Style", ExpressionType.Equal, "Coupe")
            };

            CarRules = carRules.GenerateRules<ICar>();
        }

        private static void CompilePersonRules()
        {

            List<Rule> personRules = new List<Rule>
            {
                new Rule("FirstName", null, "S", "StartsWith"),
                new Rule("LastName", null, "Rule", "Contains")
            };

            PersonRules = personRules.GenerateRules<IPerson>();
        }
    }
}
