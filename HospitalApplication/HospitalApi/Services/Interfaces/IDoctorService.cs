using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<bool> CreateAsync(Doctor doctor);

        Task<Doctor?> GetAsync(Guid id, bool withPassword = false);

        Task<ICollection<Doctor>?> GetAllAsync(bool onlyActive);

        Task<bool> UpdateAsync(Doctor doctor);
    }
}
