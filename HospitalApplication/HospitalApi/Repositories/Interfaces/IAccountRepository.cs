using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<bool> CreateAsync(AccountDto account);
        Task<AccountDto?> GetAsync(Guid id);
        Task<ICollection<AccountDto>?> GetAllAsync();
        Task<bool> UpdateAsync(AccountDto patient);
    }
}
