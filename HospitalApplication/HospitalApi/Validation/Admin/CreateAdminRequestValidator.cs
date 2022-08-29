using FluentValidation;
using HospitalApi.Contracts.Requests.Admin;

namespace HospitalApi.Validation.Admin
{
    public class CreateAdminRequestValidator : AbstractValidator<CreateAdminRequest>
    {
        public CreateAdminRequestValidator()
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
}
