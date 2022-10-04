using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Domain;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services;

public class AccountantService : IAccountantService
{
    private readonly IAccountantRepository _accountantRepository;

    public AccountantService(IAccountantRepository accountantRepository)
    {
        _accountantRepository = accountantRepository;
    }
    public async Task<bool> CreateAsync(Accountant accountant)
    {
        var existingUser = await _accountantRepository.GetAsync(accountant.Id.Value);
        if (existingUser is not null)
        {
            var message = $"A accountant with id {accountant.Id} already exists";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Patient), message)
            });
        }

        var accountantDto = accountant.ToAccountantDto();
        return await _accountantRepository.CreateAsync(accountantDto);
    }

    public async Task<ICollection<Accountant>?> GetAllAsync()
    {
        var list = await _accountantRepository.GetAllAsync();
        var retVal = new List<Accountant>();
        if (list != null)
        {
            foreach (var item in list)
            {
                retVal.Add(item.ToAccountant());
            }
            return retVal;
        }
        return new List<Accountant>();
    }

    public async Task<Accountant?> GetAsync(Guid id, bool withPassword = false)
    {
        var accountantDto = await _accountantRepository.GetAsync(id);
        if (withPassword)
        {
            return accountantDto?.ToAccountantWithPassword();
        }
        return accountantDto?.ToAccountant();
    }

    public async Task<bool> UpdateAsync(Accountant accountant)
    {
        var accountantDto = accountant.ToAccountantDto();
        return await _accountantRepository.UpdateAsync(accountantDto);
    }
}

