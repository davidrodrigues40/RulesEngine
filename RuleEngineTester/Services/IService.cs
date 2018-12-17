using System.Collections.Generic;

namespace RuleEngineTester.Services
{
    public interface IService<T>
    {
        IEnumerable<T> Get();

        IList<string> ValidateList(IEnumerable<T> items);
    }
}
