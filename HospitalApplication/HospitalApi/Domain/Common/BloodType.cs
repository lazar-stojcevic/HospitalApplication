using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common;

public class BloodType : ValueOf<string, BloodType>
{
    private static readonly Regex BloodTypeRegex =
        new("^(A|B|AB|0)[+-]$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    protected override void Validate()
    {
        if (!BloodTypeRegex.IsMatch(Value))
        {
            var message = $"{Value} is not a valid bloodType";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(BloodType), message)
            });
        }
    }
}

