using System.ComponentModel.DataAnnotations;

namespace Demo.Infra.Validators;

public class TenantIdValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
            return false;

        // Validate if the Tenant ID is a valid GUID
        return Guid.TryParse(value.ToString(), out _);
    }
}