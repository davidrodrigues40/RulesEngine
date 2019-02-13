using RuleEngineTester.Entities;
using RuleEngineTester.Services;
using RulesEngine.Enumerators;

namespace RuleEngineTester.Extensions
{
    public static class CarExtension
    {
        public static (Car item, RuleEnumerators.RuleStatus status, string[] failedFields) Validate(this Car item, ICarRulesProcessor processor)
        {
            return processor.Process(item);
        }
    }
}
