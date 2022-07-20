using HospitalApi.Contracts.Data;
using System.Collections.Generic;

namespace HospitalApi.Repositories;

public interface IPatientRepository
{
    Task<bool> CreateAsync(PatientDto patient);

    Task<PatientDto?> GetAsync(Guid id);

    Task<ICollection<PatientDto>?> GetAllAsync();

    Task<bool> UpdateAsync(PatientDto patient);

    Task<bool> DeleteAsync(Guid id);
}
