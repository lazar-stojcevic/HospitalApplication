using HospitalApi.Domain.Common.Financial;

namespace HospitalApi.Domain
{
    public class Account
    {
        public AccountId Id { get; init; } = AccountId.From(Guid.NewGuid());
        public AccountNumber AccountNumber { get; init; } = default!;
        public Balance Balance { get; init; } = default!;
        public PatientId PatientId { get; init; } = null!;
    }
}
