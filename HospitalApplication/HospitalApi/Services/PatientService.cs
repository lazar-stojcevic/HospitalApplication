﻿using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Domain;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;
using System.Security.Claims;

namespace HospitalApi.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public PatientService(IPatientRepository patientRepository, IHttpContextAccessor contextAccessor)
    {
        _patientRepository = patientRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> CreateAsync(Patient patient)
    {
        var existingUser = await _patientRepository.GetAsync(patient.Id.Value);
        if (existingUser is not null)
        {
            var message = $"A user with id {patient.Id} already exists";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Patient), message)
            });
        }

        var patientDto = patient.ToPatientDto();
        return await _patientRepository.CreateAsync(patientDto);
    }

    public async Task<Patient?> GetAsync(Guid id, bool withPassword = false)
    {
        var patientDto = await _patientRepository.GetAsync(id);
        if (withPassword)
        {
            return patientDto?.ToPatientWithPassword();
        }
        return patientDto?.ToPatient();
    }

    public async Task<bool> UpdateAsync(Patient patient)
    {
        var patientDto = patient.ToPatientDto();
        return await _patientRepository.UpdateAsync(patientDto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _patientRepository.DeleteAsync(id);
    }

    public async Task<ICollection<Patient>?> GetAllAsync()
    {
        var list = await _patientRepository.GetAllAsync();
        var retVal = new List<Patient>();
        if (list != null)
        {
            foreach (var item in list)
            {
                retVal.Add(item.ToPatient());
            }
            return retVal;
        }
        return new List<Patient>();
    }
}
