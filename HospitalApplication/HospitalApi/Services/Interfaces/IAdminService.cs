using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces;

public interface IAdminService
{
    Task<bool> CreateAsync(Admin admin);

    Task<Admin?> GetAsync(Guid id);

    Task<ICollection<Admin>?> GetAllAsync();

    Task<bool> UpdateAsync(Admin admin);
}

