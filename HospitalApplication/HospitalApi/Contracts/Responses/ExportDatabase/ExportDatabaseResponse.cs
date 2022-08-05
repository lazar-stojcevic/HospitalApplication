using HospitalApi.Contracts.Data;

namespace HospitalApi.Contracts.Responses.ExportDatabase;

public class ExportDatabaseResponse
{
    public IEnumerable<PatientDto> Patients { get; init; } = Enumerable.Empty<PatientDto>();
    public IEnumerable<DoctorDto> Doctors { get; init; } = Enumerable.Empty<DoctorDto>();
    public IEnumerable<AppointmentDto> Appointments { get; init; } = Enumerable.Empty<AppointmentDto>();
    public IEnumerable<AccountDto> Accounts { get; init; } = Enumerable.Empty<AccountDto>();
}

