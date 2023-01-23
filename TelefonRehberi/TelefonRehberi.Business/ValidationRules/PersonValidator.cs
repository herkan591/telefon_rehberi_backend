using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TelefonRehberi.Entities;

namespace TelefonRehberi.Business.ValidationRules
{
    public class PersonValidator:AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be blank!");
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required!");

            RuleFor(x => x.Surname).NotNull().WithMessage("Surname is required!");

            RuleFor(x => x.CellPhone).NotEmpty().WithMessage("CepTelefonu boş bırakılamaz!");
            RuleFor(x => x.CellPhone).NotNull().WithMessage("Cep Telefonu gereklidir!");

            RuleFor(x => x.CellPhone).MaximumLength(11).MinimumLength(11).WithMessage("CellPhone must be 11 digits!");



        }

        
    }
}
