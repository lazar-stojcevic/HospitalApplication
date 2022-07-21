using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Financial
{
    public class Balance : ValueOf<double, Balance>
    {
        protected override void Validate()
        {
            if (Value <= -10000)
            {
                var message = $"{Value} is not a valid balance, because balacne can't go below 10000 RSD";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Balance), message)
            });
            }
        }
    }
}
