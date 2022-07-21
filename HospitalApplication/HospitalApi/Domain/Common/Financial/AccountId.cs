using ValueOf;

namespace HospitalApi.Domain.Common.Financial;
public class AccountId : ValueOf<Guid, AccountId>
{
    protected override void Validate()
    {
        if (Value == Guid.Empty)
        {
            throw new ArgumentException("Account Id cannot be empty", nameof(AccountId));
        }
    }
}

