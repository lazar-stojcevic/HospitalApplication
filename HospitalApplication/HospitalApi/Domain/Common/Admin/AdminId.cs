using ValueOf;

namespace HospitalApi.Domain.Common.Admin;

public class AdminId : ValueOf<Guid, AdminId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Admin Id cannot be empty", nameof(AdminId));
        }
    }
}

