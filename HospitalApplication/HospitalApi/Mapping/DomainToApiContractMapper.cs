using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;
using HospitalApi.Domain;

namespace HospitalApi.Mapping;

public static class DomainToApiContractMapper
{
    public static PatientResponse ToPatientResponse(this Patient patient)
    {
        return new PatientResponse
        {
            Id = patient.Id.Value,
            Email = patient.Email.Value,
            Username = patient.Username.Value,
            FirstName = patient.FirstName.Value,
            Surname = patient.Surname.Value,
            BloodType = patient.BloodType.Value,
            Weight = patient.Weight.Value,
            Height = patient.Height.Value,
            Adress = patient.Adress.Value,
            Gender = patient.Gender.Value,
            PersonalNumber = patient.PersonalNumber.Value,
            PhoneNumber = patient.PhoneNumber.Value,
            DateOfBirth = patient.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            AccountId = patient.AccountId?.Value ?? null,
        };
    }

    public static AccountResponse ToAccountResponse(this Account account)
    {
        return new AccountResponse
        {
            Id = account.Id.Value,
            AccountNumber = account.AccountNumber.Value,
            Balance = account.Balance.Value,
            PatientId = account.PatientId.Value,
        };
    }

    public static GetAllPatientsResponse ToPatientsResponse(this IEnumerable<Patient> patients)
    {
        return new GetAllPatientsResponse
        {
            Patients = patients.Select(x => new PatientResponse
            {
                Id = x.Id.Value,
                Email = x.Email.Value,
                Username = x.Username.Value,
                FirstName = x.FirstName.Value,
                Surname = x.Surname.Value,
                BloodType = x.BloodType.Value,
                Weight = x.Weight.Value,
                Height = x.Height.Value,
                Adress = x.Adress.Value,
                Gender = x.Gender.Value,
                PersonalNumber = x.PersonalNumber.Value,
                PhoneNumber = x.PhoneNumber.Value,
                DateOfBirth = x.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
                AccountId = x.AccountId.Value,
            })
        };
    }

    public static GetAllAccountsResponse ToAccountsResponse(this IEnumerable<Account> accounts)
    {
        return new GetAllAccountsResponse
        {
            Accounts = accounts.Select(account => new AccountResponse
            {
                Id = account.Id.Value,
                AccountNumber = account.AccountNumber.Value,
                Balance = account.Balance.Value,
                PatientId = account.PatientId.Value,
            })
        };
    }

    public static DoctorResponse ToDoctorResponse(this Doctor doctor)
    {
        return new DoctorResponse
        {
            Id = doctor.Id.Value,
            Email = doctor.Email.Value,
            Username = doctor.Username.Value,
            FirstName = doctor.FirstName.Value,
            Surname = doctor.Surname.Value,
            PersonalNumber = doctor.PersonalNumber.Value,
            PhoneNumber = doctor.PhoneNumber.Value,
            DateOfBirth = doctor.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
            MedicalSpeciality = doctor.MedicalSpeciality.Value,
        };
    }

    public static GetAllDoctorsResponse ToDoctorsResponse(this IEnumerable<Doctor> doctors)
    {
        return new GetAllDoctorsResponse
        {
            Doctors = doctors.Select(doctor => new DoctorResponse
            {
                Id = doctor.Id.Value,
                Email = doctor.Email.Value,
                Username = doctor.Username.Value,
                FirstName = doctor.FirstName.Value,
                Surname = doctor.Surname.Value,
                PersonalNumber = doctor.PersonalNumber.Value,
                PhoneNumber = doctor.PhoneNumber.Value,
                DateOfBirth = doctor.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),
                MedicalSpeciality = doctor.MedicalSpeciality.Value,
            })
        };
    }

    public static AppointmentResponse ToAppointmentResponse(this Appointment appointment)
    {
        return new AppointmentResponse
        {
            Id = appointment.Id.Value,
            DoctorId = appointment.DoctorId.Value,
            PatientId = appointment.PatientId.Value,
            EndTime = appointment.EndTime.Value,
            StartTime = appointment.StartTime.Value,
            Report = appointment.Report?.Value ?? "",
            Price = appointment.Price?.Value ?? 0,
        };
    }

    public static MultipleAppointmentsResponse ToMultipleAppointmentResponse(this IEnumerable<Appointment> appointments)
    {
        return new MultipleAppointmentsResponse
        {
            Appointments = appointments.Select(appointment => new AppointmentResponse
            {
                Id = appointment.Id.Value,
                DoctorId = appointment.DoctorId.Value,
                PatientId = appointment.PatientId.Value,
                EndTime = appointment.EndTime.Value,
                StartTime = appointment.StartTime.Value,
                Report = appointment.Report?.Value ?? string.Empty,
                Price = appointment.Price?.Value ?? 0
            })
        };
    }
}
