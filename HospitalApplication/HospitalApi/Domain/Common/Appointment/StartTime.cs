using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Appointment;

public class StartTime : ValueOf<DateTime, StartTime>
{
    protected override void Validate()
    {
        if (Value > DateTime.Now.AddDays(100))
        {
            const string message = "You can not appoint term in more than 100 days in future";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(StartTime), message)
            });
        }
    }
}

