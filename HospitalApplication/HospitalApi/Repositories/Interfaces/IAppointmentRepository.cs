using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IAppointmentRepository
{
    Task<bool> CreateAsync(AppointmentDto appointment);
    Task<AppointmentDto?> GetAsync(Guid id);
    Task<ICollection<AppointmentDto>?> GetAllAsync();
    Task<bool> UpdateAsync(AppointmentDto patient);
}

