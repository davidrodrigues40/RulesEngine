using GenFu;
using RuleEngineTester.Entities;
using RuleEngineTester.Extensions;
using RulesEngine.Enumerators;
using System.Collections.Generic;
using System.Linq;

namespace RuleEngineTester.Services
{
    public class CarService : BaseRuleService, ICarService
    {
        private string[] _makes = { "Ford", "Chevrolet", "Lincoln", "VW", "BMW", "Audi" };
        private string[] _styles = { "Coupe", "Sedan", "SUV", "Convertible" };
        private int[] _years = { 1993, 1980, 2003, 2005, 2015, 2018 };
        private ICarRulesProcessor _ruleProcessor;
        public CarService(ICarRulesProcessor ruleProcessor)
        {
            _ruleProcessor = ruleProcessor;
            _ruleProcessor.AddRules(RuleCreatorService.CarExpressions);
        }
        public IEnumerable<Car> Get()
        {
            var cars = new List<Car>();

            GenFu.GenFu.Configure<Car>()
                .Fill(c => c.Year)
                .WithRandom(_years)
                .Fill(c => c.Make)
                .WithRandom(_makes)
                .Fill(c => c.Style)
                .WithRandom(_styles);


            cars.AddRange(A.ListOf<Car>(10));
            cars.Add(new Car(2018, "Ford", "Coupe"));

            return cars;
        }

        private string WriteInfoValidated((Car item, RuleEnumerators.RuleStatus status, string[] failedFields) value)
        {
            if (value.failedFields == null || value.failedFields.Count() == 0)
                return $"Vehicle: {value.item.Year.ToString()} {value.item.Make} {value.item.Style} - PASSED";

            return $"Vehicle: {value.item.Year.ToString()} {value.item.Make} {value.item.Style} - FAILED. REASON: {GenerateFailedFieldMessage(value.failedFields, RuleCreatorService.CarRules)}";
        }

        public IList<string> ValidateList(IEnumerable<Car> items)
        {
            IList<string> output = new List<string>();
            items.ToList().ForEach(car =>
            {
                output.Add(WriteInfoValidated(car.Validate(_ruleProcessor)));
                output.Add(" ");
            });
            return output;
        }
    }
}
