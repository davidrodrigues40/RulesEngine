using RulesEngine.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngineTester.Services
{
    public abstract class BaseRuleService
    {
        protected string GenerateFailedFieldMessage(string[] failedFields, List<Rule> rules)
        {
            List<Rule> failedRules;
            List<string> messages = new List<string>();

            if (failedFields != null)
            {
                failedRules = rules.Where(r => failedFields.Contains(r.ComparisonPredicate)).ToList();

                if (failedRules.Any())
                    failedRules.ForEach(r => messages.Add($"{r.ComparisonPredicate} not {r.ComparisonOperator}{r.CustomComparer} {r.ComparisonValue} "));

                return string.Join(", ", messages);
                //if (failedFields.Length == 1)
                //    return $"{failedFields[0]} is invalid.";
                //else
                //    return $"{string.Join(", ", failedFields)} are invalid.";
            }
            return string.Empty;
        }
    }
}
