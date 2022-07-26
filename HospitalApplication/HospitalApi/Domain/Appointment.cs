using HospitalApi.Domain.Common.Appointment;
using HospitalApi.Domain.Common.Doctor;
using HospitalApi.Domain.Common.Patient;

namespace HospitalApi.Domain;

public class Appointment
{
    public AppointmentId Id { get; init; }  = AppointmentId.From(Guid.NewGuid());
    public PatientId PatientId { get; init; } = null!;
    public DoctorId DoctorId { get; init; } = null!;
    public Report? Report { get; init; } = default!;
    public StartTime StartTime { get; init; } = default!;
    public EndTime EndTime { get; init; } = default!;
}

