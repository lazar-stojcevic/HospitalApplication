using ValueOf;

namespace HospitalApi.Domain.Common.Appointment;

public class AppointmentId : ValueOf<Guid, AppointmentId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Appointment Id cannot be empty", nameof(AppointmentId));
        }
    }
}

