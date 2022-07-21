using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IPatientRepository
{
    Task<bool> CreateAsync(PatientDto patient);

    Task<PatientDto?> GetAsync(Guid id);

    Task<ICollection<PatientDto>?> GetAllAsync();

    Task<bool> UpdateAsync(PatientDto patient);

    Task<bool> DeleteAsync(Guid id);
}
