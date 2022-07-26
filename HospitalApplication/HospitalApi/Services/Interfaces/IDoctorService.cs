﻿using HospitalApi.Domain;

namespace HospitalApi.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<bool> CreateAsync(Doctor doctor);

        Task<Doctor?> GetAsync(Guid id);

        Task<ICollection<Doctor>?> GetAllAsync();

        Task<bool> UpdateAsync(Doctor doctor);
    }
}