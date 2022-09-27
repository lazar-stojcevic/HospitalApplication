namespace HospitalApi.Contracts.Responses.Appointment;

public class AppointmentResponse
{
    public Guid Id { get; set; } = default!;
    public Guid PatientId { get; set; } = default!;
    public Guid DoctorId { get; set; } = default!;
    public string DoctorName { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string DoctorSpeciality { get; set; } = string.Empty;
    public string Report { get; set; } = default!;
    public string Price { get; set; } = default!;
    public DateTime StartTime { get; set; } = default!;
    public DateTime EndTime { get; set; } = default!;
}

