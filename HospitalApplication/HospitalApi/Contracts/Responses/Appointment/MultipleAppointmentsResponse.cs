namespace HospitalApi.Contracts.Responses.Appointment;

public class MultipleAppointmentsResponse
{
    public ICollection<AppointmentResponse> Appointments { get; init; } = new List<AppointmentResponse>();
}

