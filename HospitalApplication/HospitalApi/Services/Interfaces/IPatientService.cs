using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IPatientService
{
    Task<bool> CreateAsync(Patient patient);

    Task<Patient?> GetAsync(Guid id, bool withPassword = false);

    Task<ICollection<Patient>?> GetAllAsync();

    Task<bool> UpdateAsync(Patient patient);

    Task<bool> DeleteAsync(Guid id);
}
