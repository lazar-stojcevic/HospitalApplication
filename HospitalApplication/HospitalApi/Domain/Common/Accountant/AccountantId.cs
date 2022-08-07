using ValueOf;

namespace HospitalApi.Domain.Common.Accountant;

public class AccountantId : ValueOf<Guid, AccountantId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Accountant Id cannot be empty", nameof(AccountantId));
        }
    }
}

