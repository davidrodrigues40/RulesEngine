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
            _ruleProcessor.AddRules(RuleCreatorService.PersonExpressions);
        }

        public IEnumerable<Customer> Get()
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

        public (Customer item, RuleEnumerators.RuleStatus status, string[] failedFields) ValidateList(Customer item)
        {
            return _ruleProcessor.Process(item);
        }

        public IList<string> ValidateList(IEnumerable<Customer> items)
        {
            IList<string> output = new List<string>();
            items.ToList().ForEach(person =>
            {
                (Customer item, RuleEnumerators.RuleStatus status, string[] failedFields) validation = person.Validate(_ruleProcessor);
                output.Add(WriteInfoValidated(validation));
                output.Add(" ");
            });
            return output;
        }

        private string WriteInfoValidated((Customer item, RuleEnumerators.RuleStatus status, string[] failedFields) value)
        {
            if (value.failedFields == null || value.failedFields.Count() == 0)
                return $"Person: {value.item.FirstName} {value.item.MiddleName} {value.item.LastName} - PASSED";

            return $"Person: {value.item.FirstName} {value.item.MiddleName} {value.item.LastName} - FAILED. REASON: {GenerateFailedFieldMessage(value.failedFields, RuleCreatorService.PersonRules)}";
        }
    }
}
