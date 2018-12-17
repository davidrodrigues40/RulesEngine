using RulesEngine.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RulesEngine.Services
{
    public class RuleProcessor<T> : IRuleProcessor<T>
    {
        private List<Func<T, bool>> _rules;

        public RuleProcessor()
        {
        }

        public void AddRules(List<Func<T, bool>> rules)
        {
            _rules = rules;
        }

        public (T item, RuleEnumerators.RuleStatus status, string[] failedFields) Process(T input)
        {
            if (_rules == null || _rules.Count == 0)
                throw new MissingFieldException("Missing Rules");

            var failures = _rules.Where(rule => !rule(input));
            if (failures == null || failures.Count() == 0)
                return (input, RuleEnumerators.RuleStatus.Passed, null);
            else
                return (input, RuleEnumerators.RuleStatus.Failed, failures.Select(f => f.Method.Name).ToArray());
        }
    }
}
