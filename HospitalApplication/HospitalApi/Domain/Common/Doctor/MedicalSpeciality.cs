using FluentValidation;
using FluentValidation.Results;
using ValueOf;

namespace HospitalApi.Domain.Common.Doctor
{
    public class MedicalSpeciality : ValueOf<string, MedicalSpeciality>
    {
        protected override void Validate()
        {
            if (!Value.Equals("Immunologists") &&
                !Value.Equals("Anesthesiologists") &&
                !Value.Equals("Cardiologists") &&
                !Value.Equals("Surgeon") &&
                !Value.Equals("Dermatologists") &&
                !Value.Equals("Gastroenterologists") &&
                !Value.Equals("Hematologists") &&
                !Value.Equals("Internists") &&
                !Value.Equals("Nephrologists") &&
                !Value.Equals("Neurologists") &&
                !Value.Equals("Gynecologists") &&
                !Value.Equals("Oncologists") &&
                !Value.Equals("Ophthalmologists") &&
                !Value.Equals("Otolaryngologists") &&
                !Value.Equals("Pediatricians") &&
                !Value.Equals("Physiatrists") &&
                !Value.Equals("Psychiatrists") &&
                !Value.Equals("Pulmonologists") &&
                !Value.Equals("Radiologists") &&
                !Value.Equals("Rheumatologists") &&
                !Value.Equals("Urologists") &&
                !Value.Equals("Internists")
                )
            {
                var message = $"{Value} is not a valid medical speciality";
                throw new ValidationException(message, new[]
                {
                new ValidationFailure(nameof(MedicalSpeciality), message)
            });
            }
        }
    }
}
