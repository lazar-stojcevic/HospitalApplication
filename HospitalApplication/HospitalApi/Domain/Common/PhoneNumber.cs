using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common;

public class PhoneNumber : ValueOf<string, PhoneNumber>
{
    private static readonly Regex PhoneNumberRegex =
        new("[0-9]{8,12}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!PhoneNumberRegex.IsMatch(Value))
        {
            var message = $"{Value} is not a valid phone number";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(PhoneNumber), message)
            });
        }
    }
}


