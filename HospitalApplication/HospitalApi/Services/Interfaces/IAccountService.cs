using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAccountService
{
    Task<Account?> GetAsync(Guid id);

    Task<ICollection<Account>?> GetAllAsync();

    Task<bool> UpdateAsync(Account account, double amount);
    Task<Guid> CreateAccount(Guid patientId);
}

