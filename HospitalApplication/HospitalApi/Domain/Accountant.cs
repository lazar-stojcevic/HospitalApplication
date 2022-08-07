﻿using HospitalApi.Domain.Common.Accountant;
using HospitalApi.Domain.Common.Shared;

namespace HospitalApi.Domain;

public class Accountant
{
    public AccountantId Id { get; init; } = AccountantId.From(Guid.NewGuid());
    public Username Username { get; init; } = default!;
    public FirstName FirstName { get; init; } = default!;
    public Surname Surname { get; init; } = default!;
    public PersonalNumber PersonalNumber { get; init; } = default!;
    public PhoneNumber PhoneNumber { get; init; } = default!;
    public EmailAddress Email { get; init; } = default!;
    public DateOfBirth DateOfBirth { get; init; } = default!;
    public string Password { get; private set; } = default!;

    public void SetPassword(string password) => Password = password;
}

