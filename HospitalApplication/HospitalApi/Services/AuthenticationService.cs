using HospitalApi.Contracts.Data;
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
    private readonly IConfiguration _configuration;

    public AuthenticationService(IDoctorRepository doctorRepository, IPatientRepository patientRepository, IConfiguration configuration)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _configuration = configuration;
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
}

