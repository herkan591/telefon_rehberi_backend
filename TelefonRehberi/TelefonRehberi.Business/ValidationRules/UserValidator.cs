using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TelefonRehberi.Business.Dtos;
using TelefonRehberi.Entities;

namespace TelefonRehberi.Business.ValidationRules
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            
            RuleFor(x => x.RefreshToken).NotEmpty();
            RuleFor(x => x.RefreshTokenEndDate).NotEmpty();

        }
    }
}
