using FluentValidation;
using HospitalApi.Contracts.Requests.Appointment;

namespace HospitalApi.Validation.Appointment;

public class FinishAppointmentRequestValidator : AbstractValidator<FinishAppointmentRequest>
{
    public FinishAppointmentRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Report).NotEmpty();
    }
}

