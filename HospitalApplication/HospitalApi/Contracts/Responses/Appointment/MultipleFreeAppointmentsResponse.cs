namespace HospitalApi.Contracts.Responses.Appointment
{
    public class MultipleFreeAppointmentsResponse
    {
        public IEnumerable<FreeAppointmentResponse> FreeAppointments { get; init; } = Enumerable.Empty<FreeAppointmentResponse>();
    }
}
