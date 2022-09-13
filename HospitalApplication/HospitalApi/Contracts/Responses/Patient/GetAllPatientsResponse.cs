namespace HospitalApi.Contracts.Responses.Patient;

public class GetAllPatientsResponse
{
    public ICollection<PatientResponse> Patients { get; init; } = new List<PatientResponse>();
}
