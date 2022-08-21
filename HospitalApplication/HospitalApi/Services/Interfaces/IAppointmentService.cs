using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAppointmentService
{
    Task<bool> CreateAsync(Appointment appointment);

    Task<Appointment?> GetAsync(Guid id);

    Task<ICollection<Appointment>?> GetAllAsync();

    Task<ICollection<Appointment>?> GetPatientsAppointmentsAsync(Guid patientId);
    Task<ICollection<Appointment>?> GetDoctorsFutureAppointmentsAsync(Guid doctorId);

    Task<bool> UpdateAsync(Appointment appointment);
    Task<ICollection<FreeAppointmentResponse>?> GetFreeAppointementsForPatientAndDoctor(Guid patientId, Guid doctorId, DateTime date, int appointmentLenght);
}

