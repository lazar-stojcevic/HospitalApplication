using FluentValidation;
using FluentValidation.Results;
using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Appointment;
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

    public async Task<ICollection<Appointment>?> GetDoctorsFutureAppointmentsAsync(Guid doctorId)
    {
        var list = await _appointmentRepository.GetFutureAppointmentsForDoctor(doctorId);
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

    public async Task<ICollection<FreeAppointmentResponse>?> GetFreeAppointementsForPatientAndDoctor(Guid patientId, Guid doctorId, DateTime date, int appointmentLenght)
    {
        var patientAppointments = await _appointmentRepository.GetAppointmentsForPatientByDate(patientId, date);
        var doctorAppointments = await _appointmentRepository.GetAppointmentsForDoctorByDate(doctorId, date);

        return GetFreeAppointments(
            patientAppointments ?? new List<AppointmentDto>(),
            doctorAppointments ?? new List<AppointmentDto>(),
            date, 
            appointmentLenght).ToList();
    }

    public async Task<bool> UpdateAsync(Appointment appointment)
    {
        var appointmentDto = appointment.ToAppointmentDto();
        return await _appointmentRepository.UpdateAsync(appointmentDto);
    }

    private IEnumerable<FreeAppointmentResponse> GetFreeAppointments(
        ICollection<AppointmentDto> patientAppointments,
        ICollection<AppointmentDto> doctorAppointments,
        DateTime date,
        int appointmentLenght)
    {
        foreach(var appointment in GeneretateAppointmentsForDate(date, appointmentLenght))
        {
            if (!(patientAppointments.Any(x => appointment.StartTime <= x.EndTime && appointment.EndTime >= x.StartTime)
                && doctorAppointments.Any(x => appointment.StartTime <= x.EndTime && appointment.EndTime >= x.StartTime)))
            {
                yield return appointment;
            }
        }
    }

    private IEnumerable<FreeAppointmentResponse> GeneretateAppointmentsForDate(DateTime date, int appointmentLenght)
    {
        var startTime = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
        var endTime = startTime.AddMinutes(appointmentLenght);
        while (endTime.Hour < 16)
        {
            yield return new FreeAppointmentResponse()
            {
                StartTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second),
                EndTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second),
            };
            startTime = startTime.AddMinutes(10);
            endTime = endTime.AddMinutes(10);
        }
    }
}

