using FluentValidation;
using HospitalApi.Contracts.Requests.Accountant;

namespace HospitalApi.Validation.Accountant;

public class CreateAccountantRequestValidator : AbstractValidator<CreateAccountantRequest>
{
    public CreateAccountantRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.PersonalNumber).NotEmpty();
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
    }
}

