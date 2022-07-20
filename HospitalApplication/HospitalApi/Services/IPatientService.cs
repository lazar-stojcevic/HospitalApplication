using HospitalApi.Domain;

namespace HospitalApi.Services;

public interface IPatientService
{
    Task<bool> CreateAsync(Patient patient);

    Task<Patient?> GetAsync(Guid id);

    Task<ICollection<Patient>?> GetAllAsync();

    Task<bool> UpdateAsync(Patient patient);

    Task<bool> DeleteAsync(Guid id);
}
