using ValueOf;

namespace HospitalApi.Domain.Common.Doctor;

public class DoctorId : ValueOf<Guid, DoctorId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Doctor Id cannot be empty", nameof(DoctorId));
        }
    }
}

