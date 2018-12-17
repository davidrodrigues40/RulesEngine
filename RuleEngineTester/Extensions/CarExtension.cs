using RuleEngineTester.Entities;
using RuleEngineTester.Services;
using RulesEngine.Enumerators;

namespace RuleEngineTester.Extensions
{
    public static class CarExtension
    {
        public static (ICar item, RuleEnumerators.RuleStatus status, string[] failedFields) Validate(this ICar item, ICarRulesProcessor processor)
        {
            return processor.Process(item);
        }
    }
}
