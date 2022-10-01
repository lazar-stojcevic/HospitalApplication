using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Domain;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<bool> CreateAsync(Doctor doctor)
        {
            var existingUser = await _doctorRepository.GetAsync(doctor.Id.Value);
            if (existingUser is not null)
            {
                var message = $"A doctor with id {doctor.Id} already exists";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(Patient), message)
            });
            }

            var patientDto = doctor.ToDoctorDto();
            return await _doctorRepository.CreateAsync(patientDto);
        }

        public async Task<ICollection<Doctor>?> GetAllAsync()
        {
            var list = await _doctorRepository.GetAllAsync();
            var retVal = new List<Doctor>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    retVal.Add(item.ToDoctor());
                }
                return retVal;
            }
            return new List<Doctor>();
        }

        public async Task<Doctor?> GetAsync(Guid id, bool withPassword = false)
        {
            var doctorDto = await _doctorRepository.GetAsync(id);
            if (withPassword)
            {
                return doctorDto?.ToDoctorWithPassword();
            }
            return doctorDto?.ToDoctor();
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            var doctorDto = doctor.ToDoctorDto();
            return await _doctorRepository.UpdateAsync(doctorDto);
        }
    }
}
