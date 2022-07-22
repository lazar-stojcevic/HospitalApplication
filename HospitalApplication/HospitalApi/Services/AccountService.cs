using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Domain;
using HospitalApi.Domain.Common.Financial;
using HospitalApi.Domain.Common.Patient;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services;

public class AccountService : IAccountService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IAccountRepository _accountRepository;

    private readonly static Random random = new ();

    public AccountService(IPatientRepository patientRepository, IAccountRepository accountRepository)
    {
        _patientRepository = patientRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Guid> CreateAccount(Guid patientId)
    {
        var existingUser = await _patientRepository.GetAsync(patientId);
        if (existingUser is null)
        {
            var message = $"A user with id {patientId} doesn't exists";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Patient), message)
            });
        }

        var account = new Account
        {
            Id = AccountId.From(Guid.NewGuid()),
            AccountNumber = AccountNumber.From($"123{RandomString(10)}"),
            Balance = Balance.From(0),
            PatientId = PatientId.From(patientId)
        };

        if( await _accountRepository.CreateAsync(account.ToAccountDto()))
        {
            existingUser.AccountId = AccountId.From(account.Id.Value).ToString();
            await _patientRepository.UpdateAsync(existingUser);
            return account.Id.Value;
        }
        throw new Exception("Account failed to create");
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

    public static string RandomString(int length)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

