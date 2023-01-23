using System;
using System.Collections.Generic;
using System.Text;

namespace TelefonRehberi.Business.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
