namespace HospitalApi.Contracts.Responses.Appointment
{
    public class MultipleFreeAppointmentsResponse
    {
        public ICollection<FreeAppointmentResponse> FreeAppointments { get; init; } = new List<FreeAppointmentResponse>();
    }
}
