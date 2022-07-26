namespace HospitalApi.Contracts.Responses.Appointment;

public class MultipleAppointmentsResponse
{
    public IEnumerable<AppointmentResponse> Appointments { get; init; } = Enumerable.Empty<AppointmentResponse>();
}

