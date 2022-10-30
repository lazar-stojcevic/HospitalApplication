using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Patient
{
    public class Adress : ValueOf<string, Adress>
    {
        protected override void Validate()
        {
            if (Value.Length < 4 || Value.Length > 256)
            {
                var message = $"{Value} is not a valid adress";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Adress), message)
            });
            }
        }
    }
}
