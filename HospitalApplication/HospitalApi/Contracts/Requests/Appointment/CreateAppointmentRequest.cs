namespace HospitalApi.Contracts.Requests.Appointment;

public class CreateAppointmentRequest
{
    public string PatientId { get; init; } = null!;
    public string DoctorId { get; init; } = null!;
    public DateTime StartTime { get; init; } = default!;
    public DateTime EndTime { get; init; } = default!;
}

