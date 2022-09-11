using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Login;
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
            return new LoginResponse { Token = GenerateJwtForDoctor(doctor), Role = "DOCTOR" };
        }

        var patient = await _patientRepository.GetByUsername(login.Username);

        if (patient != null && BCrypt.Net.BCrypt.Verify(login.Password, patient.Password))
        {
            return new LoginResponse { Token = GenerateJwtForPatient(patient), Role = "PATIENT" };
        }

        var accountant = await _accountantRepository.GetByUsername(login.Username);

        if (accountant != null && BCrypt.Net.BCrypt.Verify(login.Password, accountant.Password))
        {
            return new LoginResponse { Token = GenerateJwtForAccountant(accountant), Role = "ACCOUNTANT" };
        }

        var admin = await _adminRepository.GetByUsername(login.Username);

        if (admin != null && BCrypt.Net.BCrypt.Verify(login.Password, admin.Password))
        {
            return new LoginResponse { Token = GenerateJwtForAdmin(admin), Role = "ADMIN" };
        }
        return new LoginResponse { Token = "", Role = "" };
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private string GenerateJwtForDoctor(DoctorDto doctor)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, doctor.Username),
            new Claim(ClaimTypes.Role, "DOCTOR")
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
            new Claim(ClaimTypes.Role, "PATIENT")
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
            new Claim(ClaimTypes.Role, "ACCOUNTANT")
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

    private string GenerateJwtForAdmin(AdminDto admin)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, admin.Username),
            new Claim(ClaimTypes.Role, "ADMIN")
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
            case UserType.Admin:
                var admin = await _adminRepository.GetByUsername(username);
                return admin == null;
            default:
                return false;
        }
    }
}

