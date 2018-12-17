namespace RuleEngineTester.Services
{
    public abstract class BaseRuleService
    {
        protected string GenerateFailedFieldMessage(string[] failedFields)
        {
            if (failedFields != null)
            {
                if (failedFields.Length == 1)
                    return $" {failedFields[0]} is invalid.";
                else
                    return $" {string.Join(", ", failedFields)} are invalid.";
            }
            return string.Empty;
        }
    }
}
