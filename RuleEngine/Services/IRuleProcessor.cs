using RulesEngine.Enumerators;
using System;
using System.Collections.Generic;

namespace RulesEngine.Services
{
    public interface IRuleProcessor<T>
    {
        List<Func<T, bool>> Rules { get; }
        (T item, RuleEnumerators.RuleStatus status, string[] failedFields) Process(T input);
        void AddRules(List<Func<T, bool>> rules);
    }
}