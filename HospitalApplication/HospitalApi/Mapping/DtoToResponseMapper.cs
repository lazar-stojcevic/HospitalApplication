using HospitalApi.Contracts.Data;
using HospitalApi.Contracts.Responses.Accountant;
using HospitalApi.Contracts.Responses.Appointment;
using HospitalApi.Contracts.Responses.Doctor;
using HospitalApi.Contracts.Responses.Financial;
using HospitalApi.Contracts.Responses.Patient;

namespace HospitalApi.Mapping
{
    public static class DtoToResponseMapper
    {
        public static IEnumerable<AppointmentResponse> ToMultipleAppointmentResponse(this IEnumerable<AppointmentDto> appointments)
        {
            var response = new List<AppointmentResponse>();
            response.AddRange(appointments.Select(appointment => new AppointmentResponse
            {
                Id = Guid.Parse(appointment.Id),
                DoctorId = Guid.Parse(appointment.DoctorId),
                PatientId = Guid.Parse(appointment.PatientId),
                EndTime = appointment.EndTime,
                StartTime = appointment.StartTime,
                Report = appointment.Report,
                Price = appointment.Price
            }));
            return response;
        }

        public static IEnumerable<PatientResponse> ToPatientsResponse(this IEnumerable<PatientDto> patients)
        {
            var response = new List<PatientResponse>();
            response.AddRange(patients.Select(patient => new PatientResponse
            {
                Id = Guid.Parse(patient.Id),
                AccountId = Guid.Parse(patient.AccountId),
                Adress = patient.Adress,
                BloodType = patient.BloodType,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                FirstName = patient.FirstName,
                Surname = patient.Surname,
                Gender = patient.Gender,
                Height = patient.Height,
                Weight = patient.Weight,
                PersonalNumber = patient.PersonalNumber,
                PhoneNumber = patient.PhoneNumber,
                Username = patient.Username
            }));
            return response;
        }

        public static IEnumerable<DoctorResponse> ToDoctorsResponse(this IEnumerable<DoctorDto> doctors)
        {
            var response = new List<DoctorResponse>();
            response.AddRange(doctors.Select(doctor => new DoctorResponse
            {
                Id = Guid.Parse(doctor.Id),
                DateOfBirth = doctor.DateOfBirth,
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                Surname = doctor.Surname,
                PersonalNumber = doctor.PersonalNumber,
                PhoneNumber = doctor.PhoneNumber,
                Username = doctor.Username,
                MedicalSpeciality = doctor.MedicalSpeciality
            }));
            return response;
        }

        public static IEnumerable<AccountantResponse> ToAccountantsResponse(this IEnumerable<AccountantDto> accountants)
        {
            var response = new List<AccountantResponse>();
            response.AddRange(accountants.Select(accountant => new AccountantResponse
            {
                Id = Guid.Parse(accountant.Id),
                DateOfBirth = accountant.DateOfBirth,
                Email = accountant.Email,
                FirstName = accountant.FirstName,
                Surname = accountant.Surname,
                PersonalNumber = accountant.PersonalNumber,
                PhoneNumber = accountant.PhoneNumber,
                Username = accountant.Username,
            }));
            return response;
        }

        public static IEnumerable<AccountResponse> ToAccountsResponse(this IEnumerable<AccountDto> accounts)
        {
            var response = new List<AccountResponse>();
            response.AddRange(accounts.Select(account => new AccountResponse
            {
                Id = Guid.Parse(account.Id),
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                PatientId = Guid.Parse(account.PatientId)
            }));
            return response;
        }
    }
}
