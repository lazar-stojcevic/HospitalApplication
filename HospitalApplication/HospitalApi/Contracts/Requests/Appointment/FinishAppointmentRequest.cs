namespace HospitalApi.Contracts.Requests.Appointment;

public class FinishAppointmentRequest
{
    public Guid Id { get; init; }
    public string Report { get; init; } = null!;
}

