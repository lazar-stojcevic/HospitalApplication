using FluentValidation;
using HospitalApi.Contracts.Requests.Financial;

namespace HospitalApi.Validation.Account;

public class ChangeAccountBalanceValidator : AbstractValidator<ChangeAccountBalanceRequest>
{
    public ChangeAccountBalanceValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Change).NotEmpty();
    }
}

