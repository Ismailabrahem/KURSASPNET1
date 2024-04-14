using System.ComponentModel.DataAnnotations;

namespace Silicon_1.Filters;

public class CheckboxValidation : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is bool b && b;
    }
}
