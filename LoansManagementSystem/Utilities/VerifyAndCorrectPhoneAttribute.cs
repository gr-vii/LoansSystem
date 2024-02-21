using System.ComponentModel.DataAnnotations;

namespace LoansManagementSystem.Utilities;

public class VerifyAndCorrectPhoneAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var phoneNumber = value as string;

        if (phoneNumber == null)
        {
            throw new BadHttpRequestException("Phone number is required");
        }

        var (isValid, correctedPhoneNumber) = phoneNumber.VerifyAndCorrectPhone();

        if (!isValid)
        {
            throw new BadHttpRequestException("Phone number is not valid");
        }

        if (validationContext.MemberName != null)
        {
            var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);

            property?.SetValue(validationContext.ObjectInstance, correctedPhoneNumber, null);
        }

        return ValidationResult.Success;
    }
}