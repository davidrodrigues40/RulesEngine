using GenFu;
using RuleEngineTester.Entities;
using RuleEngineTester.Extensions;
using RulesEngine.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngineTester.Services
{
    public class PersonService : BaseRuleService, IPersonService
    {
        private IPersonRuleProcessor _ruleProcessor;
        public PersonService(IPersonRuleProcessor processor)
        {
            _ruleProcessor = processor;
            _ruleProcessor.AddRules(RuleCreatorService.PersonRules);
        }

        public IEnumerable<IPerson> Get()
        {
            List<Customer> customers = new List<Customer>();

            GenFu.GenFu.Configure<Customer>()
                .Fill(c => c.FirstName)
                .AsFirstName()
                .Fill(c => c.LastName)
                .AsLastName()
                .Fill(c => c.MiddleName)
                .AsFirstName();

            customers.AddRange(A.ListOf<Customer>(10));
            customers.Add(new Customer("Should", "Pass", "Rules"));

            return customers;
        }

        public (IPerson item, RuleEnumerators.RuleStatus status, string[] failedFields) ValidateList(IPerson item)
        {
            return _ruleProcessor.Process(item);
        }

        public IList<string> ValidateList(IEnumerable<IPerson> items)
        {
            IList<string> output = new List<string>();
            items.ToList().ForEach(person =>
            {
                output.Add(WriteStatus(person.Validate(_ruleProcessor)));
            });
            return output;
        }

        private string WriteStatus((IPerson item, RuleEnumerators.RuleStatus status, string[] failedFields) value)
        {
            var msg = $"Person: {value.item.FirstName} {value.item.MiddleName} {value.item.LastName} - {value.status.ToString()} the rules engine check!";

            msg += GenerateFailedFieldMessage(value.failedFields);

            return msg;
        }
    }
}
