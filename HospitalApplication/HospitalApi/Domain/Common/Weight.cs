using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common
{
    public class Weight : ValueOf<double, Weight>
    {
        protected override void Validate()
        {
            if (Value <= 0)
            {
                var message = $"{Value} is not a weight";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Weight), message)
            });
            }
        }
    }
}
