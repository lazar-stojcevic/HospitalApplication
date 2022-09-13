using FastEndpoints.Security;
using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Login;
using HospitalApi.Domain.Types;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;

namespace HospitalApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IAccountantRepository _accountantRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IDoctorRepository doctorRepository, IPatientRepository patientRepository, IConfiguration configuration, IAccountantRepository accountantRepository, IAdminRepository adminRepository)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _configuration = configuration;
        _accountantRepository = accountantRepository;
        _adminRepository = adminRepository;
    }
    public async Task<LoginResponse> AuthenticateUser(LoginDto login)
    {
        var doctor = await _doctorRepository.GetByUsername(login.Username);

        if (doctor != null && BCrypt.Net.BCrypt.Verify(login.Password, doctor.Password))
        {
            return new LoginResponse { Token = GenerateJwtForDoctor(doctor), Role = "DOCTOR", UserId = Guid.Parse(doctor.Id) };
        }

        var patient = await _patientRepository.GetByUsername(login.Username);

        if (patient != null && BCrypt.Net.BCrypt.Verify(login.Password, patient.Password))
        {
            return new LoginResponse { Token = GenerateJwtForPatient(patient), Role = "PATIENT", UserId = Guid.Parse(patient.Id) };
        }

        var accountant = await _accountantRepository.GetByUsername(login.Username);

        if (accountant != null && BCrypt.Net.BCrypt.Verify(login.Password, accountant.Password))
        {
            return new LoginResponse { Token = GenerateJwtForAccountant(accountant), Role = "ACCOUNTANT", UserId = Guid.Parse(accountant.Id) };
        }

        var admin = await _adminRepository.GetByUsername(login.Username);

        if (admin != null && BCrypt.Net.BCrypt.Verify(login.Password, admin.Password))
        {
            return new LoginResponse { Token = GenerateJwtForAdmin(admin), Role = "ADMIN", UserId = Guid.Parse(admin.Id) };
        }
        return new LoginResponse { Token = "", Role = "" };
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateJwtForDoctor(DoctorDto doctor)
    {
        var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration.GetSection("Secret:Token").Value,
                expireAt: DateTime.UtcNow.AddDays(1),
                roles: new[] { "DOCTOR" },
                claims: new[] { ("Username", doctor.Username) });

        return jwtToken;
    }

    private string GenerateJwtForPatient(PatientDto patient)
    {
        var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration.GetSection("Secret:Token").Value,
                expireAt: DateTime.UtcNow.AddDays(1),
                roles: new[] { "PATIENT" },
                claims: new[] { ("Username", patient.Username) });

        return jwtToken;
    }

    private string GenerateJwtForAccountant(AccountantDto accountant)
    {
        var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration.GetSection("Secret:Token").Value,
                expireAt: DateTime.UtcNow.AddDays(1),
                roles: new[] { "ACCOUNTANT" },
                claims: new[] { ("Username", accountant.Username) });

        return jwtToken;
    }

    private string GenerateJwtForAdmin(AdminDto admin)
    {
        var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration.GetSection("Secret:Token").Value,
                expireAt: DateTime.UtcNow.AddDays(1),
                roles: new[] { "ADMIN" },
                claims: new[] { ("Username", admin.Username) });

        return jwtToken;
    }

    public async Task<bool> IsUsernameUnique(string username, UserType userType)
    {
        switch (userType)
        {
            case UserType.Patient: 
                var customer = await _patientRepository.GetByUsername(username);
                return customer == null;
            case UserType.Doctor:
                var doctor = await _doctorRepository.GetByUsername(username);
                return doctor == null;
            case UserType.Accountant:
                var accoutnant = await _accountantRepository.GetByUsername(username);
                return accoutnant == null;
            case UserType.Admin:
                var admin = await _adminRepository.GetByUsername(username);
                return admin == null;
            default:
                return false;
        }
    }
}

