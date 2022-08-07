using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IDoctorRepository
{
    Task<bool> CreateAsync(DoctorDto doctor);
    Task<DoctorDto?> GetAsync(Guid id);
    Task<DoctorDto?> GetByUsername(string username);
    Task<ICollection<DoctorDto>?> GetAllAsync();
    Task<bool> UpdateAsync(DoctorDto doctor);
}

