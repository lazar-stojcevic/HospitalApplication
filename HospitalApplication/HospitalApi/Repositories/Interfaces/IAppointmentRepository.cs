using HospitalApi.Contracts.Data;

namespace HospitalApi.Repositories.Interfaces;

public interface IAppointmentRepository
{
    Task<bool> CreateAsync(AppointmentDto appointment);
    Task<AppointmentDto?> GetAsync(Guid id);
    Task<ICollection<AppointmentDto>?> GetAllAsync();
    Task<bool> UpdateAsync(AppointmentDto patient);
    Task<ICollection<AppointmentDto>?> GetAppointmentsForPatient(Guid patientId);
    Task<ICollection<AppointmentDto>?> GetAppointmentsForDoctor(Guid doctorId);
    Task<ICollection<AppointmentDto>?> GetAppointmentsForDoctorByDate(Guid doctorId, DateTime date);
    Task<ICollection<AppointmentDto>?> GetAppointmentsForPatientByDate(Guid patientId, DateTime date);
}

