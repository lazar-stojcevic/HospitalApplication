using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAppointmentService
{
    Task<bool> CreateAsync(Appointment appointment);

    Task<Doctor?> GetAsync(Guid id);

    Task<ICollection<Doctor>?> GetAllAsync();

    Task<bool> UpdateAsync(Appointment appointment);
}

