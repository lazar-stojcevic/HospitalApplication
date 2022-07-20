namespace HospitalApi.Contracts.Responses;

public class GetAllPatientsResponse
{
    public IEnumerable<PatientResponse> Patients { get; init; } = Enumerable.Empty<PatientResponse>();
}
