namespace HospitalApi.Contracts.Responses.Doctor;

public class GetAllDoctorsResponse
{
    public ICollection<DoctorResponse> Doctors { get; init; } = new List<DoctorResponse>();
}

