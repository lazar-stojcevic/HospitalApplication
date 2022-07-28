using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAppointmentService
{
    Task<bool> CreateAsync(Appointment appointment);

    Task<Appointment?> GetAsync(Guid id);

    Task<ICollection<Appointment>?> GetAllAsync();

    Task<ICollection<Appointment>?> GetPatientsAppointmentsAsync(Guid patientId);

    Task<bool> UpdateAsync(Appointment appointment);
}

