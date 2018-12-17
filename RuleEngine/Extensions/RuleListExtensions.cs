using RulesEngine;
using RulesEngine.Entities;
using System;
using System.Collections.Generic;

namespace RuleEngine.Extensions
{
    public static class RuleListExtensions
    {
        public static List<Func<T, bool>> GenerateRules<T>(this List<Rule> rules)
        {
            return RuleCompiler.CompileRules<T>(rules);
        }
    }
}
