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
            _ruleProcessor.AddRules(RuleCreatorService.CarRules);
        }
        public IEnumerable<ICar> Get()
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

        private string WriteStatus((ICar item, RuleEnumerators.RuleStatus status, string[] failedFields) value)
        {
            var msg = $"Vehicle: {value.item.Year.ToString()} {value.item.Make} {value.item.Style} - {value.status.ToString()} the rules engine check!";

            msg += GenerateFailedFieldMessage(value.failedFields);

            return msg;
        }

        public IList<string> ValidateList(IEnumerable<ICar> items)
        {
            IList<string> output = new List<string>();
            items.ToList().ForEach(car =>
            {
                output.Add(WriteStatus(car.Validate(_ruleProcessor)));
            });
            return output;
        }
    }
}
