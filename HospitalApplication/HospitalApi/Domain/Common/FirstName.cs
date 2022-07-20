using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common
{
    public class FirstName : ValueOf<string, FirstName>
    {
        private static readonly Regex FirstNameRegex =
            new("^[a-z ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void Validate()
        {
            if (!FirstNameRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid first name";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(FirstName), message)
            });
            }
        }
    }
}
