using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Domain;
using HospitalApi.Mapping;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;

    public AppointmentService(IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public async Task<bool> CreateAsync(Appointment appointment)
    {
        var existingPatient = await _patientRepository.GetAsync(appointment.PatientId.Value);
        if (existingPatient is null)
        {
            var message = $"A patient with id {appointment.PatientId.Value} don't exists";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Patient), message)
            });
        }

        var existingDoctor = await _doctorRepository.GetAsync(appointment.DoctorId.Value);
        if (existingPatient is null)
        {
            var message = $"A doctor with id {appointment.DoctorId.Value} don't exists";
            throw new ValidationException(message, new[]
            {
                new ValidationFailure(nameof(Doctor), message)
            });
        }

        var appointmentDto = appointment.ToAppointmentDto();
        return await _appointmentRepository.CreateAsync(appointmentDto);
    }

    public async Task<ICollection<Appointment>?> GetAllAsync()
    {
        var list = await _appointmentRepository.GetAllAsync();
        var retVal = new List<Appointment>();
        if (list != null)
        {
            foreach (var item in list)
            {
                retVal.Add(item.ToAppointment());
            }
            return retVal;
        }
        return new List<Appointment>();
    }

    public async Task<Appointment?> GetAsync(Guid id)
    {
        var appointmentDto = await _appointmentRepository.GetAsync(id);
        return appointmentDto?.ToAppointment();
    }

    public async Task<ICollection<Appointment>?> GetPatientsAppointmentsAsync(Guid patientId)
    {
        var list = await _appointmentRepository.GetAppointmentsForPatient(patientId);
        var retVal = new List<Appointment>();
        if (list != null)
        {
            foreach (var item in list)
            {
                retVal.Add(item.ToAppointment());
            }
            return retVal;
        }
        return new List<Appointment>();
    }

    public async Task<bool> UpdateAsync(Appointment appointment)
    {
        var appointmentDto = appointment.ToAppointmentDto();
        return await _appointmentRepository.UpdateAsync(appointmentDto);
    }
}

