namespace HospitalApi.Contracts.Responses.Patient;

public class GetAllPatientsResponse
{
    public IEnumerable<PatientResponse> Patients { get; init; } = Enumerable.Empty<PatientResponse>();
}
