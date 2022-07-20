using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common
{
    public class PersonalNumber : ValueOf<string, PersonalNumber>
    {
        private static readonly Regex FullNameRegex =
            new("[0-9]{13}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void Validate()
        {
            if (!FullNameRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid personal number";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(PersonalNumber), message)
            });
            }
        }
    }
}
