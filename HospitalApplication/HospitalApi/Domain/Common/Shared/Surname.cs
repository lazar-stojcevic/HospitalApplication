using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Shared
{
    public class Surname : ValueOf<string, Surname>
    {
        private static readonly Regex SurnameRegex =
            new("^[a-z ,.'-]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void Validate()
        {
            if (!SurnameRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid surname";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Surname), message)
            });
            }
        }
    }

}
