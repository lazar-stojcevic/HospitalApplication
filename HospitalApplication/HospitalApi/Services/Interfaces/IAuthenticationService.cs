using HospitalApi.Contracts.Data;

namespace HospitalApi.Services.Interfaces;

public interface IAuthenticationService
{
    Task<string> AuthenticateUser(LoginDto appointment);
}

