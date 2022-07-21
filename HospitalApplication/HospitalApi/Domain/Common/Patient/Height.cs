using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Patient
{
    public class Height : ValueOf<double, Height>
    {
        protected override void Validate()
        {
            if (Value <= 15)
            {
                var message = $"{Value} is not a height";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Height), message)
            });
            }
        }
    }
}
