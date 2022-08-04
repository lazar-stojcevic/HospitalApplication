using HospitalApi.Contracts.Requests.Appointment;
using HospitalApi.Contracts.Requests.Doctor;
using HospitalApi.Contracts.Requests.Financial;
using HospitalApi.Contracts.Requests.Patient;
using HospitalApi.Domain;
using HospitalApi.Domain.Common.Appointment;
using HospitalApi.Domain.Common.Doctor;
using HospitalApi.Domain.Common.Financial;
using HospitalApi.Domain.Common.Patient;
using HospitalApi.Domain.Common.Shared;

namespace HospitalApi.Mapping;

public static class ApiContractToDomainMapper
{
    public static Patient ToPatient(this CreatePatientRequest request)
    {
        return new Patient
        {
            Id = PatientId.From(Guid.NewGuid()),
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FirstName = FirstName.From(request.FirstName),
            Surname = Surname.From(request.Surname),
            Adress = Adress.From(request.Adress),
            BloodType = BloodType.From(request.BloodType),
            Gender = Gender.From(request.Gender),
            Height = Height.From(request.Height),
            Weight = Weight.From(request.Weight),
            PersonalNumber = PersonalNumber.From(request.PersonalNumber),
            PhoneNumber = PhoneNumber.From(request.PhoneNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth)),
        };
    }

    public static Patient ToPatient(this UpdatePatientRequest request)
    {
        return new Patient
        {
            Id = PatientId.From(request.Id),
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FirstName = FirstName.From(request.FirstName),
            Surname = Surname.From(request.Surname),
            Adress = Adress.From(request.Adress),
            BloodType = BloodType.From(request.BloodType),
            Gender = Gender.From(request.Gender),
            Height = Height.From(request.Height),
            Weight = Weight.From(request.Weight),
            PersonalNumber = PersonalNumber.From(request.PersonalNumber),
            PhoneNumber = PhoneNumber.From(request.PhoneNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth))
        };
    }

    public static Account ToAccount(this ChangeAccountBalanceRequest request)
    {
        return new Account
        {
            Id = AccountId.From(request.Id),
        };
    }

    public static Doctor ToDoctor(this CreateDoctorRequest request)
    {
        return new Doctor
        {
            Id = DoctorId.From(Guid.NewGuid()),
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FirstName = FirstName.From(request.FirstName),
            Surname = Surname.From(request.Surname),
            PersonalNumber = PersonalNumber.From(request.PersonalNumber),
            PhoneNumber = PhoneNumber.From(request.PhoneNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth)),
            MedicalSpeciality = MedicalSpeciality.From(request.MedicalSpeciality),
        };
    }

    public static Doctor ToDoctor(this UpdateDoctorRequest request)
    {
        return new Doctor
        {
            Email = EmailAddress.From(request.Email),
            Username = Username.From(request.Username),
            FirstName = FirstName.From(request.FirstName),
            Surname = Surname.From(request.Surname),
            PersonalNumber = PersonalNumber.From(request.PersonalNumber),
            PhoneNumber = PhoneNumber.From(request.PhoneNumber),
            DateOfBirth = DateOfBirth.From(DateOnly.FromDateTime(request.DateOfBirth)),
            MedicalSpeciality = MedicalSpeciality.From(request.MedicalSpeciality),
        };
    }

    public static Appointment ToAppointment(this CreateAppointmentRequest request)
    {
        var appointment = new Appointment
        {
            Id = AppointmentId.From(Guid.NewGuid()),
            DoctorId = DoctorId.From(Guid.Parse(request.DoctorId)),
            PatientId = PatientId.From(Guid.Parse(request.PatientId)),
            EndTime = EndTime.From(request.EndTime),
            StartTime = StartTime.From(request.StartTime),
        };
        appointment.InitAppointment();
        return appointment;
    }
}
