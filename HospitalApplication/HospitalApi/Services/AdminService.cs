using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Domain;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<bool> CreateAsync(Admin admin)
        {
            var existingUser = await _adminRepository.GetAsync(admin.Id.Value);
            if (existingUser is not null)
            {
                var message = $"A accountant with id {admin.Id} already exists";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Patient), message)
            });
            }

            var accountantDto = admin.ToAdminDto();
            return await _adminRepository.CreateAsync(accountantDto);
        }

        public async Task<ICollection<Admin>?> GetAllAsync()
        {
            var list = await _adminRepository.GetAllAsync();
            var retVal = new List<Admin>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    retVal.Add(item.ToAdmin());
                }
                return retVal;
            }
            return new List<Admin>();
        }

        public async Task<Admin?> GetAsync(Guid id)
        {
            var adminDto = await _adminRepository.GetAsync(id);
            return adminDto?.ToAdmin();
        }

        public async Task<bool> UpdateAsync(Admin admin)
        {
            var adminDto = admin.ToAdminDto();
            return await _adminRepository.UpdateAsync(adminDto);
        }
    }
}
