using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IAdminRepository
{
    Task<bool> CreateAsync(AdminDto admin);
    Task<AdminDto?> GetAsync(Guid id);
    Task<AdminDto?> GetByUsername(string username);
    Task<ICollection<AdminDto>?> GetAllAsync();
    Task<bool> UpdateAsync(AdminDto admin);
}

