using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common
{
    public class Adress : ValueOf<string, Adress>
    {
        private static readonly Regex AdressRegex =
            new(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void Validate()
        {
            if (!AdressRegex.IsMatch(Value))
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
