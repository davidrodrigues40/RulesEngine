using RulesEngine.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesEngine.Services
{
    public class RuleProcessor<T> : IRuleProcessor<T>
    {
        public List<Func<T, bool>> Rules { get; private set; }

        public RuleProcessor()
        {
        }

        public void AddRules(List<Func<T, bool>> rules)
        {
            Rules = rules;
        }

        public (T item, RuleEnumerators.RuleStatus status, string[] failedFields) Process(T input)
        {
            if (Rules == null || Rules.Count == 0)
                throw new MissingFieldException("Missing Rules");

            var failures = Rules.Where(rule => !rule(input));

            if (failures == null || failures.Count() == 0)
                return (input, RuleEnumerators.RuleStatus.Passed, null);
            else
                return (input, RuleEnumerators.RuleStatus.Failed, failures.Select(f => f.Method.Name).ToArray());
        }
    }
}
