namespace HospitalApi.Contracts.Responses.Appointment;

public class FreeAppointmentResponse
{
    public DateTime StartTime { get; init; } = default!;
    public DateTime EndTime { get; init; } = default!;
}

