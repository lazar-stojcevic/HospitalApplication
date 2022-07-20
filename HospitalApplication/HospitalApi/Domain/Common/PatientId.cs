using ValueOf;

namespace HospitalApi.Domain.Common;

public class PatientId : ValueOf<Guid, PatientId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Patient Id cannot be empty", nameof(PatientId));
        }
    }
}
