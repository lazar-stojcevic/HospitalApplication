namespace HospitalApi.Contracts.Requests.Appointment;

public class GetAvailableAppointmentsForDate
{
    public Guid PatientId { get; init; }
    public Guid DoctorId { get; init; }
    public DateTime Date { get; init; }
    public int AppointmentLength { get; init; }
}

