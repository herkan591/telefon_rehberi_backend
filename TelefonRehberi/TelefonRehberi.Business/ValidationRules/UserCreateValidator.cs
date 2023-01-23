using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TelefonRehberi.Business.Dtos;

namespace TelefonRehberi.Business.ValidationRules
{
    public class UserCreateValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
        }
    }
}
