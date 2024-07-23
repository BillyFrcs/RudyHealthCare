using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace RudyHealthCare.Models.Patients
{
    public class PatientsModelValidator : AbstractValidator<PatientsModel>
    {
        public PatientsModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.IdentityNumber).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.PlaceOfBirth).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.Age).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.Gender).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.Profession).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.ComplaintsOfPain).NotEmpty().WithMessage("The field is required");
            RuleFor(x => x.DiagnoseResult).NotEmpty().WithMessage("The field is required");
        }
    }
}