using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TelefonRehberi.Business.Dtos;

namespace TelefonRehberi.Business.ValidationRules
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();

        }

    }
}
