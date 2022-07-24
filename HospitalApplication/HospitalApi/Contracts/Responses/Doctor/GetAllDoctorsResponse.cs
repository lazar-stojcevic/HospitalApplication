namespace HospitalApi.Contracts.Responses.Doctor;

public class GetAllDoctorsResponse
{
    public IEnumerable<DoctorResponse> Doctors { get; init; } = Enumerable.Empty<DoctorResponse>();
}

