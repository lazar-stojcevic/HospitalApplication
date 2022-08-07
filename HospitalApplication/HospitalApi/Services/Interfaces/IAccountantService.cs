using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAccountantService
{
    Task<bool> CreateAsync(Accountant accountant);

    Task<Accountant?> GetAsync(Guid id);

    Task<ICollection<Accountant>?> GetAllAsync();

    Task<bool> UpdateAsync(Accountant accountant);
}

