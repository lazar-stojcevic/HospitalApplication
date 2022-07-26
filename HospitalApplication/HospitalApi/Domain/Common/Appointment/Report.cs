using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Appointment;

public class Report : ValueOf<string, Report>
{
    protected override void Validate()
    {
        /*
        if (Value.Length > 128)
        {
            var message = $"Report must have more than 128 characters";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Report), message)
            });
        }
        */
    }
}

