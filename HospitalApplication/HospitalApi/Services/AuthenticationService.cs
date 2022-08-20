using HospitalApi.Contracts.Data;
using HospitalApi.Domain.Types;
using HospitalApi.Repositories.Interfaces;
using HospitalApi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HospitalApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IAccountantRepository _accountantRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationService(IDoctorRepository doctorRepository, IPatientRepository patientRepository, IConfiguration configuration, IAccountantRepository accountantRepository)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _configuration = configuration;
        _accountantRepository = accountantRepository;
    }
    public async Task<string> AuthenticateUser(LoginDto login)
    {
        var doctor = await _doctorRepository.GetByUsername(login.Username);

        if (doctor != null && BCrypt.Net.BCrypt.Verify(login.Password, doctor.Password))
        {
            return GenerateJwtForDoctor(doctor);
        }

        var patient = await _patientRepository.GetByUsername(login.Username);

        if (patient != null && BCrypt.Net.BCrypt.Verify(login.Password, patient.Password))
        {
            return GenerateJwtForPatient(patient);
        }

        var accountant = await _accountantRepository.GetByUsername(login.Username);

        if (accountant != null && BCrypt.Net.BCrypt.Verify(login.Password, accountant.Password))
        {
            return GenerateJwtForAccountant(accountant);
        }
        return string.Empty;
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateJwtForDoctor(DoctorDto doctor)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, doctor.Username)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Secret:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private string GenerateJwtForPatient(PatientDto patient)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, patient.Username),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Secret:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private string GenerateJwtForAccountant(AccountantDto accountant)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, accountant.Username),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Secret:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
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
            default:
                return false;
        }
    }
}

