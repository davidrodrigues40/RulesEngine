using Microsoft.Extensions.DependencyInjection;
using RuleEngineTester.Entities;
using RuleEngineTester.Services;
using System;
using System.Collections.Generic;

namespace RuleEngineTester
{
    public class Program
    {
        private static ICarService _carService;
        private static IPersonService _personService;
        private static IEnumerable<Car> _cars;
        private static IEnumerable<Customer> _customers;
        private static ServiceProvider _serviceProvider;
        public static void Main(string[] args)
        {
            SetupDependencyInjection();

            OnStartUp();

            Run();
        }

        private static void SetupDependencyInjection()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<ICarService, CarService>()
                .AddSingleton<IPersonService, PersonService>()
                .AddSingleton<ICarRulesProcessor, CarRulesProcessor>()
                .AddSingleton<IPersonRuleProcessor, PersonRulesProcessor>()
                .BuildServiceProvider();
        }

        private static void Run()
        {
            Console.Clear();
            GetData();
            ValidateData();

            Console.WriteLine(string.Empty);
            Console.WriteLine("Press 'R' to retry or any other key to end...");
            if (Console.ReadKey().KeyChar.ToString().ToLower() == "r")
                Run();
        }

        private static void OnStartUp()
        {
            // Compile rules
            RuleCreatorService.CreateRules();

            // load services
            _carService = _serviceProvider.GetService<ICarService>();
            _personService = _serviceProvider.GetService<IPersonService>();
        }

        private static void GetData()
        {
            // Get Mock data
            _cars = _carService.Get();
            _customers = _personService.Get();
        }

        private static void ValidateData()
        {
            // validate cars
            Console.WriteLine("Year must be greater than 2012, make must be Ford and model must be coupe");
            foreach (string output in _carService.ValidateList(_cars))
                Console.WriteLine(output);

            Console.WriteLine("----------------------------------------------------------------");

            // validate customers
            Console.WriteLine("First Name must start with the letter S");
            foreach (string output in _personService.ValidateList(_customers))
                Console.WriteLine(output);
        }
    }
}