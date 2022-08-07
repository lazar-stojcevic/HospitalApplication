using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IAccountantRepository
{
    Task<bool> CreateAsync(AccountantDto accountant);
    Task<AccountantDto?> GetAsync(Guid id);
    Task<AccountantDto?> GetByUsername(string username);
    Task<ICollection<AccountantDto>?> GetAllAsync();
    Task<bool> UpdateAsync(AccountantDto accountant);
}

