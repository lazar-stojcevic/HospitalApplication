using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Login;
using HospitalApi.Domain.Types;

namespace HospitalApi.Services.Interfaces;

public interface IAuthenticationService
{
    Task<bool> IsUsernameUnique(string username, UserType userType);
    Task<LoginResponse> AuthenticateUser(LoginDto appointment);
    string HashPassword(string password);
}

