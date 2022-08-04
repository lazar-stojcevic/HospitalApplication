using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Appointment;

public class Price : ValueOf<double, Price>
{
    protected override void Validate()
    {
        if (Value < 0)
        {
            var message = $"{Value} is not a valid price, price can not be less then 0";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Price), message)
            });
        }
    }
}

