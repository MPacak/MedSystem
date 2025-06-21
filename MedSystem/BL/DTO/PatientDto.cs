using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BL.DTO
{
    public class PatientDto
    {
        public string OIB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
    public class PatientDtoValidator : AbstractValidator<PatientDto>
    {
        public PatientDtoValidator()
        {
            RuleFor(x => x.OIB)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("OIB is required.")
                .Matches(@"^\d{11}$")
                    .WithMessage("OIB must be exactly 11 numeric digits.");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.DateOfBirth)
                .Cascade(CascadeMode.Stop)
                .Must(d => d != default(DateTime))
                    .WithMessage("Date of birth must be a valid day/month/year.")
                .LessThan(DateTime.Today)
                    .WithMessage("Date of birth must be in the past.");

            RuleFor(x => x.Gender)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Gender is required.");
        }
    }
}
