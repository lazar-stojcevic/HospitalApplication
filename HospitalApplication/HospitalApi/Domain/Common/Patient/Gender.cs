using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Patient;

public class Gender : ValueOf<string, Gender>
{
    protected override void Validate()
    {
        if (!Value.Equals("Male") && !Value.Equals("Female"))
        {
            var message = $"{Value} is not a valid gender";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Gender), message)
            });
        }
    }
}

