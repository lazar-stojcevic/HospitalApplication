namespace HospitalApi.Contracts.Responses.Appointment;

public class AppointmentResponse
{
    public Guid Id { get; init; } = default!;
    public Guid PatientId { get; init; } = default!;
    public Guid DoctorId { get; init; } = default!;
    public string Report { get; init; } = default!;
    public double Price { get; init; } = default!;
    public DateTime StartTime { get; init; } = default!;
    public DateTime EndTime { get; init; } = default!;
}

