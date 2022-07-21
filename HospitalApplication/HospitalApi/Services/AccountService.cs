using HospitalApi.Domain;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services;

public class AccountService : IAccountService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IAccountRepository _accountRepository;

    public AccountService(IPatientRepository patientRepository, IAccountRepository accountRepository)
    {
        _patientRepository = patientRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ICollection<Account>?> GetAllAsync()
    {
        var list = await _accountRepository.GetAllAsync();
        var retVal = new List<Account>();
        if (list != null)
        {
            foreach (var item in list)
            {
                retVal.Add(item.ToAccount());
            }
            return retVal;
        }
        return new List<Account>();
    }

    public async Task<Account?> GetAsync(Guid id)
    {
        var accountDto = await _accountRepository.GetAsync(id);
        return accountDto?.ToAccount();
    }

    public async Task<bool> UpdateAsync(Account account, double amount)
    {
        var accountDto = account.ToAccountDto();
        accountDto.Balance += amount;
        return await _accountRepository.UpdateAsync(accountDto);
    }
}

