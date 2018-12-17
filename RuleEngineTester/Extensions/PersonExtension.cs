using RuleEngineTester.Entities;
using RuleEngineTester.Services;
using RulesEngine.Enumerators;

namespace RuleEngineTester.Extensions
{
    public static class PersonExtension
    {
        public static (IPerson item, RuleEnumerators.RuleStatus status, string[] failedFields) Validate(this IPerson item, IPersonRuleProcessor processor)
        {
            return processor.Process(item);
        }
    }
}
