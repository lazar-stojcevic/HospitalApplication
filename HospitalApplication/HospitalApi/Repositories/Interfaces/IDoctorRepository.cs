using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IDoctorRepository
{
    Task<bool> CreateAsync(DoctorDto doctor);

    Task<DoctorDto?> GetAsync(Guid id);

    Task<ICollection<DoctorDto>?> GetAllAsync();

    Task<bool> UpdateAsync(DoctorDto doctor);
}

