using RuleEngineTester.Entities;
using RulesEngine.Services;

namespace RuleEngineTester.Services
{
    public class PersonRulesProcessor : RuleProcessor<Customer>, IPersonRuleProcessor
    {
    }
}
