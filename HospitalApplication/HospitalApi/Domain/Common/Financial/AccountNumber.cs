using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Financial
{
    public class AccountNumber : ValueOf<string, AccountNumber>
    {
        private static readonly Regex AccountNumberRegex =
            new("[0-9]{13}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override void Validate()
        {
            if (!AccountNumberRegex.IsMatch(Value))
            {
                var message = $"{Value} is not a valid phone number";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(AccountNumber), message)
            });
            }
        }
    }
}
