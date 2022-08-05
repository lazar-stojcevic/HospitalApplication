using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Appointment;

namespace HospitalApi.Mapping
{
    public static class DtoToResponseMapper
    {
        public static MultipleAppointmentsResponse ToMultipleAppointmentResponse(this IEnumerable<AppointmentDto> appointments)
        {
            return new MultipleAppointmentsResponse
            {
                Appointments = appointments.Select(appointment => new AppointmentResponse
                {
                    Id = Guid.Parse(appointment.Id),
                    DoctorId = Guid.Parse(appointment.DoctorId),
                    PatientId = Guid.Parse(appointment.PatientId),
                    EndTime = appointment.EndTime,
                    StartTime = appointment.StartTime,
                    Report = appointment.Report,
                    Price = appointment.Price
                })
            };
        }
    }
}
